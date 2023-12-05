namespace Tekton.Domain.Dtos.Responses
{
	/// <summary>
	/// VallueObject para obtener las respuestas del Handler <see cref="ProductByIdQueryHandler"/>.
	/// </summary>
	public class ProductByIdQueryResponse
	{
		/// <summary>
		/// El identificador único del producto al ejecutar <see cref="ProductAllQueryHandler"/>.
		/// </summary>
		public int ProductId { get; set; }

		/// <summary>
		///  El nombre del producto al ejecutar <see cref="ProductByIdQueryHandler"/>.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// El identificador del estado del producto al ejecutar <see cref="ProductByIdQueryHandler"/>.
		/// </summary>
		public string StatusName { get; set; }

		/// <summary>
		/// La cantidad disponible en inventario del producto al ejecutar <see cref="ProductByIdQueryHandler"/>.
		/// </summary>
		public int Stock { get; set; }

		/// <summary>
		/// Una descripción detallada del producto al ejecutar <see cref="ProductByIdQueryHandler"/>.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// El precio del producto al ejecutar <see cref="ProductByIdQueryHandler"/>.
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// El precio del producto al ejecutar <see cref="ProductByIdQueryHandler"/>.
		/// </summary>
		public decimal Discount { get; set; }

		/// <summary>
		/// El descuento aplicado al producto, expresado como un porcentaje del precio al ejecutar <see cref="ProductByIdQueryHandler"/>.
		/// </summary>
		public decimal FinalPrice { get; set; }
	}
}
