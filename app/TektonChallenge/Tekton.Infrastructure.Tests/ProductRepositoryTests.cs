using Castle.DynamicProxy.Internal;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tekton.Domain.Entities;
using Tekton.Infrastructure.Persistence;
using Tekton.Infrastructure.Repositories;

namespace Tekton.Infrastructure.Tests
{
	public class ProductRepositoryTests
	{
		[Theory]
		[InlineData("Test Product", 10, 100, 0, 1, "")]
		[InlineData("", 5, 10, 0, 1, "")]
		[InlineData("12345678901234567890123456789012345678901234567890", 50, 1000, 5, 1, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
		public async Task Create_Ok(string name, int stock, int price, int discount, int statusId, string description)
		{
			// Arrange
			var options = new DbContextOptionsBuilder<TektonDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase")
				.Options;
			var context = new TektonDbContext(options);
			var repository = new ProductRepository(context);
			var newProduct = new Product
			{
				Name = name,
				Stock = stock,
				Price = price,
				Discount = discount,
				StatusId = statusId,
				Description = description
			};

			// Act
			var result = await repository.Create(newProduct);

			// Assert
			Assert.NotEqual(0, result);
		}

		[Theory]
		[InlineData("Test Product", 10, 100)]
		[InlineData("Test Product", 10, 0)]
		[InlineData("Test Product", 0, 100)]
		[InlineData("Test Product", 0, 0)]
		[InlineData("", 10, 0)]
		[InlineData("", 10, 100)]
		[InlineData("", 0, 0)]
		public async Task Create_Error(string name, int stock, int price)
		{
			// Arrange
			var options = new DbContextOptionsBuilder<TektonDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabaseForCreateError")
				.Options;

			var context = new TektonDbContext(options);

			var repository = new ProductRepository(context);
			var product = new Product();
			if (!string.IsNullOrEmpty(name))
			{
				product.Name = name;
			}
			if (stock > 0)
			{
				product.Stock = stock;
			}
			if (price > 0)
			{
				product.Price = price;
			}

			// Act
			var result = await repository.Create(product);

			// Assert
			Assert.Equal(0, result);
		}

		[Theory]
		[InlineData("Test Product", 10, 100, 0, 1, "")]
		[InlineData("", 5, 10, 0, 1, "")]
		[InlineData("12345678901234567890123456789012345678901234567890", 50, 1000, 5, 1, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
		public async Task CreateAndFind_Ok(string name, int stock, int price, int discount, int statusId, string description)
		{
			// Arrange
			var options = new DbContextOptionsBuilder<TektonDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase")
				.Options;
			var context = new TektonDbContext(options);
			var repository = new ProductRepository(context);
			var newProduct = new Product
			{
				Name = name,
				Stock = stock,
				Price = price,
				Discount = discount,
				StatusId = statusId,
				Description = description
			};

			// Act
			var result = await repository.Create(newProduct);
			var createdProduct = await context.Products.FindAsync(result);

			// Assert
			createdProduct.Should().NotBeNull();
			createdProduct.Name.Should().Be(name);
			createdProduct.Stock.Should().Be(stock);
			createdProduct.Price.Should().Be(price);
			createdProduct.Discount.Should().Be(discount);
		}

		[Theory]
		[InlineData("Test Product", 10, 100, 0, 1, "")]
		[InlineData("", 5, 10, 0, 1, "")]
		[InlineData("12345678901234567890123456789012345678901234567890", 50, 1000, 5, 1, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
		public async Task Delete_Ok(string name, int stock, int price, int discount, int statusId, string description)
		{
			// Arrange
			var options = new DbContextOptionsBuilder<TektonDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabaseForDelete")
				.Options;
			var productId = 0;
			var resultDelete = 0;
			var newProduct = new Product
			{
				Name = name,
				Stock = stock,
				Price = price,
				Discount = discount,
				StatusId = statusId,
				Description = description
			};
			//var context = new TektonDbContext(options);

			// Act
			using (var context = new TektonDbContext(options))
			{
				context.Products.Add(newProduct);
				await context.SaveChangesAsync();
				productId = newProduct.Id;
			}

			using (var context = new TektonDbContext(options))
			{
				var repository = new ProductRepository(context);
				resultDelete = await repository.Delete(new Product(){ Id = productId });
			}

			// Assert
			resultDelete.Should().NotBe(0);
			resultDelete.Should().Be(productId);
		}

		[Theory]
		[InlineData("Test Product", 10, 100, 0, 1, "")]
		[InlineData("", 5, 10, 0, 1, "")]
		[InlineData("12345678901234567890123456789012345678901234567890", 50, 1000, 5, 1, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
		public async Task Delete_Error(string name, int stock, int price, int discount, int statusId, string description)
		{
			// Arrange
			var options = new DbContextOptionsBuilder<TektonDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabaseForDelete")
				.Options;
			var resultDelete = 0;
			var newProduct = new Product
			{
				Name = name,
				Stock = stock,
				Price = price,
				Discount = discount,
				StatusId = statusId,
				Description = description
			};
			//var context = new TektonDbContext(options);

			// Act
			using (var context = new TektonDbContext(options))
			{
				context.Products.Add(newProduct);
				await context.SaveChangesAsync();
				var productId = newProduct.Id;
			}

			using (var context = new TektonDbContext(options))
			{
				var repository = new ProductRepository(context);
				resultDelete = await repository.Delete(new Product() { Id = 9 });
			}

			// Assert
			resultDelete.Should().Be(0);
		}

		[Fact]
		public async Task GetProductsBy_Ok()
		{
			// Arrange
			var options = new DbContextOptionsBuilder<TektonDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabaseForGetProductsBy")
				.Options;

			// Act
			using (var context = new TektonDbContext(options))
			{
				var status = new Status { Id = 1, Name = "Active" };
				context.Statuses.Add(status);

				context.Products.AddRange(
					new Product { Name = "Product 1", Stock = 30, Price = 100, Discount = 5, StatusId = 1, Status = status, Description = string.Empty},
					new Product { Name = "Product 2", Stock = 20, Price = 200, Discount = 10, StatusId = 1, Status = status, Description = string.Empty },
					new Product { Name = "Product 3", Stock = 10, Price = 300, Discount = 15, StatusId = 1, Status = status, Description = string.Empty }
				);
				await context.SaveChangesAsync();
			}

			// Crea un nuevo contexto para el repositorio
			using (var context = new TektonDbContext(options))
			{
				var repository = new ProductRepository(context);
				var products = (repository.GetProductsBy(q => q.Id == 1)).ToList();

				products.Should().NotBeNull();
				products.Count.Should().Be(1);
			}
		}
	}
}