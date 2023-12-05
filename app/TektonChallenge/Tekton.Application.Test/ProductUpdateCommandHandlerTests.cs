using AutoMapper;
using FluentValidation.Results;
using FluentValidation;
using Moq;
using Tekton.Application.Handlers.Commands;
using Tekton.Application.Interfaces.Repositories;
using Tekton.Domain.Dtos.Requests;
using Tekton.Domain.Entities;
using FluentAssertions;

namespace Tekton.Application.Test
{
	public class ProductUpdateCommandHandlerTests
	{
		private readonly Mock<IProductRepository> _mockRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly Mock<IValidator<ProductUpdateCommandRequest>> _mockValidator;
		private readonly ProductUpdateCommandHandler _handler;

		public ProductUpdateCommandHandlerTests()
		{
			_mockRepository = new Mock<IProductRepository>();
			_mockMapper = new Mock<IMapper>();
			_mockValidator = new Mock<IValidator<ProductUpdateCommandRequest>>();
			_handler = new ProductUpdateCommandHandler(_mockRepository.Object, _mockMapper.Object, _mockValidator.Object);
		}

		[Fact]
		public async Task Handle_Ok()
		{
			// Arrange
			var request = new ProductUpdateCommandRequest();

			_mockValidator.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
				.ReturnsAsync(new ValidationResult());

			var product = new Product();
			_mockMapper.Setup(m => m.Map<Product>(request)).Returns(product);

			_mockRepository.Setup(r => r.Update(It.IsAny<Product>()))
				.ReturnsAsync(1);

			// Act
			var response = await _handler.Handle(request, CancellationToken.None);

			// Assert
			response.Should().NotBeNull();
			response.State.Should().Be(200);
			response.Message.Should().Be("Ok");
			response.Value.Should().NotBe(0);
			_mockRepository.Verify(r => r.Update(product), Times.Once);
		}

		[Fact]
		public async Task Handle_ErrorValidation()
		{
			// Arrange
			var request = new ProductUpdateCommandRequest();

			_mockValidator.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
				.ReturnsAsync(new ValidationResult(new[]
				{
				new ValidationFailure("SomeProperty", "Error message")
				}));

			// Act
			var response = await _handler.Handle(request, CancellationToken.None);

			// Assert
			response.Should().NotBeNull();
			response.State.Should().Be(400);
			response.Message.Should().Contain("Errores de validación:");
			response.Value.Should().Be(0);
		}

		[Fact]
		public async Task Handle_Error()
		{
			// Arrange
			var request = new ProductUpdateCommandRequest();

			_mockRepository.Setup(r => r.Update(It.IsAny<Product>()))
				.ThrowsAsync(new Exception("Database error"));

			// Act
			var response = await _handler.Handle(request, CancellationToken.None);

			// Assert
			response.Should().NotBeNull();
			response.State.Should().Be(500);
			response.Message.Should().Contain("Error interno en el servidor:");
			response.Value.Should().Be(0);
		}
	}
}
