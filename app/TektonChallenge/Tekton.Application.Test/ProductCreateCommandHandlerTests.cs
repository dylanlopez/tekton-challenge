using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Tekton.Application.Handlers.Commands;
using Tekton.Application.Interfaces.Repositories;
using Tekton.Domain.Dtos.Requests;
using Tekton.Domain.Entities;

namespace Tekton.Application.Test
{
	public class ProductCreateCommandHandlerTests
	{
		private readonly Mock<IProductRepository> _mockRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly Mock<IValidator<ProductCreateCommandRequest>> _mockValidator;
		private readonly ProductCreateCommandHandler _handler;

		public ProductCreateCommandHandlerTests()
		{
			_mockRepository = new Mock<IProductRepository>();
			_mockMapper = new Mock<IMapper>();
			_mockValidator = new Mock<IValidator<ProductCreateCommandRequest>>();
			_handler = new ProductCreateCommandHandler(_mockRepository.Object, _mockMapper.Object, _mockValidator.Object);
		}

		[Theory]
		[InlineData("Test Product", 10, 100, 0, "")]
		[InlineData("12345678901234567890123456789012345678901234567890", 50, 1000, 5, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
		public async Task Handle_Ok(string name, int stock, int price, int discount, string description)
		{
			// Arrange
			var request = new ProductCreateCommandRequest
			{
				Name = name,
				Stock = stock,
				Price = price,
				Discount = discount,
				Description = description
			};

			_mockValidator.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
				.ReturnsAsync(new ValidationResult());

			var product = new Product();
			_mockMapper.Setup(m => m.Map<Product>(request)).Returns(product);

			_mockRepository.Setup(r => r.Create(It.IsAny<Product>()))
				.ReturnsAsync(1);

			// Act
			var response = await _handler.Handle(request, CancellationToken.None);

			// Assert
			response.Should().NotBeNull();
			response.State.Should().Be(200);
			response.Message.Should().Be("Ok");
			response.Value.Should().NotBe(0);
			_mockRepository.Verify(r => r.Create(product), Times.Once);
		}

		[Theory]
		[InlineData("", 50, 1000, 5, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
		[InlineData("12345678901234567890123456789012345678901234567890", 0, 1000, 5, "")]
		[InlineData("123456789012345678901234567890123456789012345678901", 50, 1000, 5, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
		[InlineData("12345678901234567890123456789012345678901234567890", 50, 1000, 5, "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901")]
		[InlineData("123456789012345678901234567890123456789012345678901", 50, 1000, 5, "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901")]
		public async Task Handle_ErrorValidation(string name, int stock, int price, int discount, string description)
		{
			// Arrange
			var request = new ProductCreateCommandRequest
			{
				Name = name,
				Stock = stock,
				Price = price,
				Discount = discount,
				Description = description
			};

			_mockValidator.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
				.ReturnsAsync(new ValidationResult(new[]
				{
					new ValidationFailure("Name", "Name is required")
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
			var request = new ProductCreateCommandRequest();

			_mockRepository.Setup(r => r.Create(It.IsAny<Product>()))
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