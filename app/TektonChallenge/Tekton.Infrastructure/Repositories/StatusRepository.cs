using Microsoft.EntityFrameworkCore;
using System;
using Tekton.Application.Interfaces.Repositories;
using Tekton.Application.Interfaces.Services;
using Tekton.Domain.Entities;
using Tekton.Infrastructure.Persistence;

namespace Tekton.Infrastructure.Repositories
{
	/// <summary>
	/// Interfaz para operaciones de repositorio relacionadas con la entidad <see cref="Status"/>.
	/// </summary>
	public class StatusRepository : IStatusRepository
	{
		private readonly ICacheService _cacheService;
		private readonly TektonDbContext _contextFactory;

		public StatusRepository(ICacheService cacheService, TektonDbContext contextFactory)
		{
			_cacheService = cacheService;
			_contextFactory = contextFactory;
		}

		/// <summary>
		/// Obtiene un IQueryable de la entidad <see cref="Status"/>, permitiendo consultas LINQ eficientes y opcionales sin seguimiento de entidades.
		/// </summary>
		/// <param name="asNoTracking"> Booleano que indica si el seguimiento de entidades debe ser desactivado (valor por defecto: true).</param>
		/// <returns>Proveedor de consultas de la entidad <see cref="Status"/>.</returns>
		public IQueryable<Status> Query(bool asNoTracking = true)
		{
			var result = asNoTracking ? _contextFactory.Statuses.AsNoTracking() : _contextFactory.Statuses;
			return result;
		}

		/// <summary>
		/// Obtiene un IQueryable de todos los <see cref="Status"/>, con una opción de almacenamiento en caché para mejorar el rendimiento.
		/// </summary>
		/// <returns>IQueryable<Status> que representa todos los <see cref="Status"/> disponibles.</returns>
		public IQueryable<Status> GetAllStatuses()
		{
			var result = _cacheService.GetOrSet("status_cache_key", () => GetAllStatusesFromDb(true), 5);
			return result.AsQueryable();
		}

		/// <summary>
		/// Obtiene una lista de todos los <see cref="Status"/> directamente desde la base de datos.
		/// </summary>
		/// <param name="asNoTracking">Booleano que indica si el seguimiento de entidades debe ser desactivado (valor por defecto: false).</param>
		/// <returns>List<Status> que representa todos los <see cref="Status"/> disponibles.</returns>
		public List<Status> GetAllStatusesFromDb(bool asNoTracking = false)
		{
			var result = Query(asNoTracking)
				.ToList();
			return result;
		}
	}
}
