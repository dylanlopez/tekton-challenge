using System.Linq.Expressions;
using Tekton.Domain.Entities;

namespace Tekton.Application.Interfaces.Repositories
{
	/// <summary>
	/// Interfaz para operaciones de repositorio relacionadas con la entidad <see cref="Product"/>.
	/// </summary>
	public interface IProductRepository
	{
		/// <summary>
		/// Obtiene un IQueryable de la entidad <see cref="Product"/>, permitiendo consultas LINQ eficientes y opcionales sin seguimiento de entidades.
		/// </summary>
		/// <param name="asNoTracking">Booleano que indica si el seguimiento de entidades debe ser desactivado (valor por defecto: true).</param>
		/// <returns>Proveedor de consultas de la entidad <see cref="Product"/>.</returns>
		IQueryable<Product> Query(bool asNoTracking = true);

		/// <summary>
		/// Crea un nuevo Producto en la base de datos.
		/// </summary>
		/// <param name="entity">La entidad <see cref="Product"/> que será creada.</param>
		/// <returns>Id de la entidad <see cref="Product"/> que fue creada.</returns>
		Task<int> Create(Product entity);

		/// <summary>
		/// Elimina un Producto existente de la base de datos.
		/// </summary>
		/// <param name="entity">La entidad <see cref="Product"/> que será eliminada.</param>
		/// <returns>Id de la entidad <see cref="Product"/> que fue eliminada.</returns>
		Task<int> Delete(Product entity);

		/// <summary>
		/// Obtiene un IQueryable de Productos basado en una expresión de filtro.
		/// </summary>
		/// <param name="predicate">Expresión Lambda que representa el filtro a aplicar.</param>
		/// <param name="asNoTracking">Booleano que indica si el seguimiento de entidades debe ser desactivado (valor por defecto: false).</param>
		/// <returns>IQueryable<Product> correspondiente al buscado</returns>
		IQueryable<Product> GetProductsBy(Expression<Func<Product, bool>> predicate, bool asNoTracking = false);

		/// <summary>
		/// Actualiza un Producto existente en la base de datos.
		/// </summary>
		/// <param name="entity">La entidad <see cref="Product"/> que será actualizada.</param>
		/// <returns>Id de la entidad <see cref="Product"/> que fue actualizada.</returns>
		Task<int> Update(Product entity);
	}
}
