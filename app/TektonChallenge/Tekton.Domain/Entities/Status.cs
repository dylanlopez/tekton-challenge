namespace Tekton.Domain.Entities
{
	/// <summary>
	/// La clase Status representa el estado de un producto en el dominio de la aplicación. Cada estado tiene un identificador único y un nombre.
	/// </summary>
	public class Status
	{
		/// <summary>
		/// El identificador único del estado. Es la clave primaria en la base de datos.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// El nombre del estado, que describe la condición actual del producto (actualmente, "Activo", "Inactivo").
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Una colección de productos que están en este estado. Representa una relación de uno a muchos con la entidad <see cref="Product"/>.
		/// </summary>
		public virtual ICollection<Product> Products { get; set; }
	}
}
