using Tekton.Domain.Entities;

namespace Tekton.Application.Interfaces.Services
{
	/// <summary>
	/// Interfaz para el servicio de caché, utilizada para almacenar y recuperar objetos de caché, específicamente para la entidad <see cref="Status"/>.
	/// </summary>
	public interface ICacheService
	{
		/// <summary>
		/// Recupera una lista de estados (<see cref="Status"/>) del caché basándose en una clave específica. Si la clave no existe en el caché, se ejecuta la función proporcionada para obtener y almacenar los datos en el caché.
		/// </summary>
		/// <param name="cacheKey">Clave única para identificar los datos en el caché.</param>
		/// <param name="getItemCallback">Función que se ejecutará para obtener los datos si no están presentes en el caché. Esta función debe devolver una List<Status>.</param>
		/// <param name="durationInMinutes">Duración en minutos para mantener los datos en el caché antes de expirar.</param>
		/// <returns>List<Status> que representa los estados almacenados o recuperados del caché.</returns>
		List<Status> GetOrSet(string cacheKey, Func<List<Status>> getItemCallback, int durationInMinutes);
	}
}
