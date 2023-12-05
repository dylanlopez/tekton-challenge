using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tekton.Application.Interfaces.Repositories;
using Tekton.Application.Interfaces.Services;
using Tekton.Infrastructure.Persistence;
using Tekton.Infrastructure.Repositories;
using Tekton.Infrastructure.Services;

namespace Tekton.Infrastructure
{
	/// <summary>
	/// Métodos para extender la configuración los services en el <see cref="IServiceCollection"/>.
	/// </summary>
	public static class InfrastructureExtensions
	{
		/// <summary>
		/// Agrega los servicios de infraestructura, incluyendo repositorios y el contexto de la base de datos en <see cref="IServiceCollection"/>.
		/// </summary>
		/// <param name="services">Los <see cref="IServiceCollection"/> en dónde se agregarán servicios.</param>
		/// <param name="Configuration">La <see cref="IConfiguration"/> para acceder a las configuraciones.</param>
		/// <returns>El <see cref="IServiceCollection"/> original.</returns>
		/// <remarks>
		/// Este método configura el contenedor de los servicios de la aplicación para utilizar los servicios de infraestructura especificados.
		/// - Registra el contexto <see cref="TektonDbContext"/> para inyección de dependencias.
		/// - Registra los repositorios <see cref="IProductRepository"/> y <see cref="IStatusRepository"/> para inyección de dependencias.
		/// - Registra los servicios <see cref="ICacheService"/> para inyección de dependencias.
		/// </remarks>
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration Configuration)
		{
			services.AddDbContext<TektonDbContext>(options =>
				options
					.UseSqlite(Configuration.GetConnectionString("TektonDB"))
			);

			//Repositories
			services.AddTransient<IProductRepository, ProductRepository>();
			services.AddTransient<IStatusRepository, StatusRepository>();

			//Services
			services.AddTransient<ICacheService, CacheService>();
			return services;
		}
	}
}
