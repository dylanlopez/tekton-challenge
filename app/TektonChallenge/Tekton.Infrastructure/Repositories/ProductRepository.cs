using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tekton.Application.Interfaces.Repositories;
using Tekton.Domain.Entities;
using Tekton.Infrastructure.Persistence;

namespace Tekton.Infrastructure.Repositories
{
	/// <summary>
	/// Interfaz para operaciones de repositorio relacionadas con la entidad <see cref="Product"/>.
	/// </summary>
	public class ProductRepository : IProductRepository
	{
		private readonly TektonDbContext _contextFactory;

		public ProductRepository(TektonDbContext contextFactory) => _contextFactory = contextFactory;

		/// <summary>
		/// Obtiene un IQueryable de la entidad <see cref="Product"/>, permitiendo consultas LINQ eficientes y opcionales sin seguimiento de entidades.
		/// </summary>
		/// <param name="asNoTracking">Booleano que indica si el seguimiento de entidades debe ser desactivado (valor por defecto: true).</param>
		/// <returns>Proveedor de consultas de la entidad <see cref="Product"/>.</returns>
		public IQueryable<Product> Query(bool asNoTracking = true)
		{
			var result = asNoTracking ? _contextFactory.Products.AsNoTracking() : _contextFactory.Products;
			return result;
		}

		/// <summary>
		/// Crea un nuevo Producto en la base de datos.
		/// </summary>
		/// <param name="entity">La entidad <see cref="Product"/> que será creada.</param>
		/// <returns>Id de la entidad <see cref="Product"/> que fue creada.</returns>
		public async Task<int> Create(Product entity)
		{
			try
			{
				_contextFactory.Products.Add(entity);
				await _contextFactory.SaveChangesAsync();
				return entity.Id;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return 0;
			}
		}

		/// <summary>
		/// Elimina un Producto existente de la base de datos.
		/// </summary>
		/// <param name="entity">La entidad <see cref="Product"/> que será eliminada.</param>
		/// <returns>Id de la entidad <see cref="Product"/> que fue eliminada.</returns>
		public async Task<int> Delete(Product entity)
		{
			try
			{
				_contextFactory.Products.Remove(entity);
				await _contextFactory.SaveChangesAsync();
				return entity.Id;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return 0;
			}
		}

		/// <summary>
		/// Obtiene un IQueryable de Productos basado en una expresión de filtro.
		/// </summary>
		/// <param name="predicate">Expresión Lambda que representa el filtro a aplicar.</param>
		/// <param name="asNoTracking">Booleano que indica si el seguimiento de entidades debe ser desactivado (valor por defecto: false).</param>
		/// <returns>IQueryable<Product> correspondiente al buscado</returns>
		public IQueryable<Product> GetProductsBy(Expression<Func<Product, bool>> predicate, bool asNoTracking = false)
		{
			try
			{
				var result = Query(asNoTracking)
						.Where(predicate)
						.Include(i => i.Status)
					;
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
		}

		/// <summary>
		/// Actualiza un Producto existente en la base de datos.
		/// </summary>
		/// <param name="entity">La entidad <see cref="Product"/> que será actualizada.</param>
		/// <returns>Id de la entidad <see cref="Product"/> que fue actualizada.</returns>
		public async Task<int> Update(Product entity)
		{
			try
			{
				_contextFactory.Products.Update(entity);
				await _contextFactory.SaveChangesAsync();
				return entity.Id;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return 0;
			}
		}
	}
}
