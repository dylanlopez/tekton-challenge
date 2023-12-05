using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tekton.Domain.Entities;

namespace Tekton.Infrastructure.Persistence.Configurations
{
	/// <summary>
	/// Provee la configuración para el modelo de la entidad <see cref="Status"/>.
	/// </summary>
	public class StatusConfiguration : IEntityTypeConfiguration<Status>
	{
		/// <summary>
		/// Configura la entidad de tipo <see cref="Status"/>.
		/// </summary>
		/// <param name="builder">El constructor será usado para construir el modelo para esta entidad.</param>
		public void Configure(EntityTypeBuilder<Status> builder)
		{
			// Especifica la tabla de donde se mapeará la entidad.
			builder.ToTable("Status");

			// Especifica la llave primaria para esta entidad.
			builder.HasKey(x => x.Id);

			// Configurar cada propiedad de la entidad.
			builder.Property(b => b.Id)
				.HasColumnName("StatusId");

			builder.Property(b => b.Name)
				.HasColumnName("Name");

			// Configura la relación entre las entidades Product y Status.
			builder.HasMany(d => d.Products)
				.WithOne(p => p.Status)
				.HasForeignKey(d => d.StatusId)
				.HasPrincipalKey(p => p.Id);
		}
	}
}
