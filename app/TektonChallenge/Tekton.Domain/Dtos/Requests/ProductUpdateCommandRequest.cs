using MediatR;
using Tekton.Domain.Dtos.Responses;

namespace Tekton.Domain.Dtos.Requests
{
	/// <summary>
	/// VallueObject para realizar las request al Handler <see cref="ProductUpdateCommandHandler"/>.
	/// </summary>
	public class ProductUpdateCommandRequest : IRequest<Response<int>>
	{
		/// <summary>
		/// El identificador único del producto al ejecutar <see cref="ProductUpdateCommandHandler"/>.
		/// </summary>
		public int ProductId { get; set; }

		/// <summary>
		///  El nombre del producto al ejecutar <see cref="ProductUpdateCommandHandler"/>.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// El identificador del estado del producto al ejecutar <see cref="ProductUpdateCommandHandler"/>.
		/// </summary>
		public int StatusId { get; set; }

		/// <summary>
		/// La cantidad disponible en inventario del producto al ejecutar <see cref="ProductUpdateCommandHandler"/>.
		/// </summary>
		public int Stock { get; set; }

		/// <summary>
		/// Una descripción detallada del producto al ejecutar <see cref="ProductUpdateCommandHandler"/>.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// El precio del producto al ejecutar <see cref="ProductUpdateCommandHandler"/>.
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// El descuento aplicado al producto, expresado como un porcentaje del precio al ejecutar <see cref="ProductUpdateCommandHandler"/>.
		/// </summary>
		public decimal Discount { get; set; }
	}
}
