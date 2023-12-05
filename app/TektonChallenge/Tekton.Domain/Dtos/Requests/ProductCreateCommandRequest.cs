using MediatR;
using Tekton.Domain.Dtos.Responses;

namespace Tekton.Domain.Dtos.Requests
{
	/// <summary>
	/// VallueObject para realizar las request al Handler <see cref="ProductCreateCommandHandler"/>.
	/// </summary>
	public class ProductCreateCommandRequest : IRequest<Response<int>>
	{
		/// <summary>
		///  El nombre del producto al ejecutar <see cref="ProductCreateCommandHandler"/>.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// La cantidad disponible en inventario del producto al ejecutar <see cref="ProductCreateCommandHandler"/>.
		/// </summary>
		public int Stock { get; set; }

		/// <summary>
		/// Una descripción detallada del producto al ejecutar <see cref="ProductCreateCommandHandler"/>.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// El precio del producto al ejecutar <see cref="ProductCreateCommandHandler"/>.
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// El descuento aplicado al producto, expresado como un porcentaje del precio al ejecutar <see cref="ProductCreateCommandHandler"/>.
		/// </summary>
		public decimal Discount { get; set; }
	}
}
