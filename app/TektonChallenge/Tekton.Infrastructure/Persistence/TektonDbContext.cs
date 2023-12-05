using Microsoft.EntityFrameworkCore;
using Tekton.Domain.Entities;

namespace Tekton.Infrastructure.Persistence
{
	/// <summary>
	/// Contexto para la base de datos de Tekton, manejado por interacciones con EF.
	/// </summary>
	public class TektonDbContext : DbContext
	{
		/// <summary>
		/// Devuelve o setea el DbSet para las entidades de <see cref="Product"/>.
		/// </summary>
		/// <value>
		/// El DbSet representa a las entidades de <see cref="Product"/> en la base de datos.
		/// </value>
		public virtual DbSet<Product> Products { get; set; }

		/// <summary>
		/// Devuelve o setea el DbSet para las entidades de <see cref="Status"/>.
		/// </summary>
		/// <value>
		/// El DbSet representa a las entidades de <see cref="Status"/> en la base de datos.
		/// </value>
		public virtual DbSet<Status> Statuses { get; set; }

		/// <summary>
		/// Inicializa una nueva instancia de la clase <see cref="TektonDbContext"/> usando las opciones especificadas.
		/// </summary>
		/// <param name="options">Las opciones que son usadas por el DbContext.</param>
		public TektonDbContext(DbContextOptions<TektonDbContext> options) : base(options) { }

		/// <summary>
		/// Configura el esquema requerido por el contexto antes de que el modelo se bloquee y sea usado para inicializar la base de datos.
		/// Este método es llamado por cada instancia creado del contexto.
		/// </summary>
		/// <param name="modelBuilder">Define la forma de las entidades, las relaciones entre ellas, y como se mapea con la base de datos.</param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(TektonDbContext).Assembly);
			base.OnModelCreating(modelBuilder);
		}
	}
}
