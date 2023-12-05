using System.Linq.Expressions;
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
	public class ProductUpdateStockCommandHandlerTests
	{
		private readonly Mock<IProductRepository> _mockRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly Mock<IValidator<ProductUpdateStockCommandRequest>> _mockValidator;
		private readonly ProductUpdateStockCommandHandler _handler;

		public ProductUpdateStockCommandHandlerTests()
		{
			_mockRepository = new Mock<IProductRepository>();
			_mockMapper = new Mock<IMapper>();
			_mockValidator = new Mock<IValidator<ProductUpdateStockCommandRequest>>();
			_handler = new ProductUpdateStockCommandHandler(_mockRepository.Object, _mockMapper.Object, _mockValidator.Object);
		}

		[Fact]
		public async Task Handle_Ok()
		{
			// Arrange
			var request = new ProductUpdateStockCommandRequest
			{
				ProductId = 1,
				Stock = 20
			};

			var product = new Product { Id = 1, Stock = 10 };
			_mockRepository.Setup(r => r.GetProductsBy(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<bool>()))
						   .Returns(new List<Product> { product }.AsQueryable());

			_mockValidator.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
				.ReturnsAsync(new ValidationResult());

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
			var request = new ProductUpdateStockCommandRequest();

			_mockValidator.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
				.ReturnsAsync(new ValidationResult(new[]
				{
				new ValidationFailure("ProductId", "ProductId is required")
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
		public async Task Handle_ErrorNotFound()
		{
			// Arrange
			var request = new ProductUpdateStockCommandRequest
			{
				ProductId = 999
			};

			_mockRepository.Setup(r => r.GetProductsBy(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<bool>()))
						   .Returns(new List<Product>().AsQueryable());

			_mockValidator.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
				.ReturnsAsync(new ValidationResult());

			// Act
			var response = await _handler.Handle(request, CancellationToken.None);

			// Assert
			response.Should().NotBeNull();
			response.State.Should().Be(400);
			response.Message.Should().Contain("Data Not Found");
			response.Value.Should().Be(0);
		}

		[Fact]
		public async Task Handle_Error()
		{
			// Arrange
			var request = new ProductUpdateStockCommandRequest();

			_mockRepository.Setup(r => r.GetProductsBy(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<bool>()))
						   .Throws(new Exception("Database error"));

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
