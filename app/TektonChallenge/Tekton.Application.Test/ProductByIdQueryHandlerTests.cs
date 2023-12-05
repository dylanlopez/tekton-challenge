using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Linq.Expressions;
using Tekton.Application.Handlers.Queries;
using Tekton.Application.Interfaces.Repositories;
using Tekton.Domain.Dtos.Requests;
using Tekton.Domain.Dtos.Responses;
using Tekton.Domain.Entities;

namespace Tekton.Application.Test
{
	public class ProductByIdQueryHandlerTests
	{
		private readonly Mock<IProductRepository> _mockRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly Mock<IValidator<ProductByIdQueryRequest>> _mockValidator;
		private readonly ProductByIdQueryHandler _handler;

		public ProductByIdQueryHandlerTests()
		{
			_mockRepository = new Mock<IProductRepository>();
			_mockMapper = new Mock<IMapper>();
			_mockValidator = new Mock<IValidator<ProductByIdQueryRequest>>();
			_handler = new ProductByIdQueryHandler(_mockRepository.Object, _mockMapper.Object, _mockValidator.Object);
		}

		[Theory]
		[InlineData(1, "Test Product", 10, 100, 0, "")]
		public async Task Handle_Ok(int idProduct, string name, int stock, int price, int discount, string description)
		{
			// Arrange
			var request = new ProductByIdQueryRequest { ProductId = idProduct };
			var product = new Product { 
				Id = idProduct,
				Name = name,
				Stock = stock,
				Price = price,
				Discount = discount,
				Description = description
			};
			var responseDto = new ProductByIdQueryResponse();

			_mockValidator.Setup(v => v.ValidateAsync(request, CancellationToken.None))
				.ReturnsAsync(new ValidationResult());

			_mockRepository.Setup(r => r.GetProductsBy(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<bool>()))
				.Returns(new List<Product> { product }.AsQueryable());

			_mockMapper.Setup(m => m.Map<ProductByIdQueryResponse>(It.IsAny<Product>()))
				.Returns(responseDto);

			// Act
			var response = await _handler.Handle(request, CancellationToken.None);

			// Assert
			response.Should().NotBeNull();
			response.State.Should().Be(200);
			response.Message.Should().Be("Ok");
			response.Value.Should().NotBeNull();
			_mockMapper.Verify(m => m.Map<ProductByIdQueryResponse>(product), Times.Once);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(99)]
		public async Task Handle_ErrorValidation(int productId)
		{
			// Arrange
			var request = new ProductByIdQueryRequest { ProductId = productId };

			_mockValidator.Setup(v => v.ValidateAsync(request, CancellationToken.None))
				.ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("ProductId", "ProductId is required") }));

			// Act
			var response = await _handler.Handle(request, CancellationToken.None);

			// Assert
			response.Should().NotBeNull();
			response.State.Should().Be(400);
			response.Message.Should().Contain("Errores de validación:");
			response.Value.Should().BeNull();
		}

		[Fact]
		public async Task Handle_Error()
		{
			// Arrange
			var request = new ProductByIdQueryRequest();

			_mockValidator.Setup(v => v.ValidateAsync(request, CancellationToken.None))
				.ReturnsAsync(new ValidationResult());

			_mockRepository.Setup(r => r.GetProductsBy(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<bool>()))
				.Throws(new Exception("Database error"));

			// Act
			var response = await _handler.Handle(request, CancellationToken.None);

			// Assert
			response.Should().NotBeNull();
			response.State.Should().Be(500);
			response.Message.Should().Contain("Error interno en el servidor:");
			response.Value.Should().BeNull();
		}
	}
}
