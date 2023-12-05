using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tekton.Domain.Entities;

namespace Tekton.Infrastructure.Persistence.Configurations
{
	/// <summary>
	/// Provee la configuración para el modelo de la entidad <see cref="Product"/>.
	/// </summary>
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		/// <summary>
		/// Configura la entidad de tipo <see cref="Product"/>.
		/// </summary>
		/// <param name="builder">El constructor será usado para construir el modelo para esta entidad.</param>
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			// Especifica la tabla de donde se mapeará la entidad.
			builder.ToTable("Product");

			// Especifica la llave primaria para esta entidad.
			builder.HasKey(x => x.Id);

			// Configurar cada propiedad de la entidad.
			builder.Property(b => b.Id)
				.HasColumnName("ProductId");

			builder.Property(b => b.Name)
				.HasColumnName("Name")
				.HasMaxLength(50);

			builder.Property(b => b.Stock)
				.HasColumnName("Stock");

			builder.Property(b => b.Description)
				.HasColumnName("Description")
				.HasMaxLength(200);

			builder.Property(b => b.Price)
				.HasColumnName("Price");

			builder.Property(b => b.Discount)
				.HasColumnName("Discount");

			// Configura la relación entre las entidades Product y Status.
			builder.HasOne(d => d.Status)
				.WithMany(p => p.Products)
				.HasForeignKey(d => d.StatusId)
				.HasPrincipalKey(p => p.Id)
				.OnDelete(DeleteBehavior.ClientSetNull);
		}
	}
}
