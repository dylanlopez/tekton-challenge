using Tekton.Domain.Entities;

namespace Tekton.Application.Interfaces.Repositories
{
	/// <summary>
	/// Interfaz para operaciones de repositorio relacionadas con la entidad <see cref="Status"/>.
	/// </summary>
	public interface IStatusRepository
	{
		/// <summary>
		/// Obtiene un IQueryable de la entidad <see cref="Status"/>, permitiendo consultas LINQ eficientes y opcionales sin seguimiento de entidades.
		/// </summary>
		/// <param name="asNoTracking"> Booleano que indica si el seguimiento de entidades debe ser desactivado (valor por defecto: true).</param>
		/// <returns>Proveedor de consultas de la entidad <see cref="Status"/>.</returns>
		IQueryable<Status> Query(bool asNoTracking = true);

		/// <summary>
		/// Obtiene un IQueryable de todos los <see cref="Status"/>, con una opción de almacenamiento en caché para mejorar el rendimiento.
		/// </summary>
		/// <returns>IQueryable<Status> que representa todos los <see cref="Status"/> disponibles.</returns>
		IQueryable<Status> GetAllStatuses();

		/// <summary>
		/// Obtiene una lista de todos los <see cref="Status"/> directamente desde la base de datos.
		/// </summary>
		/// <param name="asNoTracking">Booleano que indica si el seguimiento de entidades debe ser desactivado (valor por defecto: false).</param>
		/// <returns>List<Status> que representa todos los <see cref="Status"/> disponibles.</returns>
		List<Status> GetAllStatusesFromDb(bool asNoTracking = false);
	}
}
