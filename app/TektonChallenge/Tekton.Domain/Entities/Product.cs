namespace Tekton.Domain.Entities
{
	/// <summary>
	/// La clase Product representa un producto en el dominio de la aplicación. Cada producto tiene varias propiedades que describen sus características y estado.
	/// </summary>
	public class Product
	{
		/// <summary>
		/// El identificador único del producto. Es la clave primaria en la base de datos.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///  El nombre del producto.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// El identificador del estado del producto. Se relaciona con la entidad <see cref="Status"/>.
		/// </summary>
		public int StatusId { get; set; }

		/// <summary>
		/// La cantidad disponible en inventario del producto.
		/// </summary>
		public int Stock { get; set; }

		/// <summary>
		/// Una descripción detallada del producto.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// El precio del producto.
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// El descuento aplicado al producto, expresado como un porcentaje del precio.
		/// </summary>
		public decimal Discount { get; set; }

		/// <summary>
		///  La propiedad de navegación que vincula el producto con su estado actual. Es una relación de uno a uno con la entidad <see cref="Status"/>.
		/// </summary>
		public virtual Status Status { get; set; }

	}
}
