using MediatR;
using Tekton.Domain.Dtos.Responses;

namespace Tekton.Domain.Dtos.Requests
{
	/// <summary>
	/// VallueObject para realizar las request al Handler <see cref="ProductUpdateStockCommandHandler"/>.
	/// </summary>
	public class ProductUpdateStockCommandRequest : IRequest<Response<int>>
	{
		/// <summary>
		/// El identificador único del producto al ejecutar <see cref="ProductUpdateStockCommandHandler"/>.
		/// </summary>
		public int ProductId { get; set; }

		/// <summary>
		/// La cantidad disponible en inventario del producto al ejecutar <see cref="ProductUpdateStockCommandHandler"/>.
		/// </summary>
		public int Stock { get; set; }
	}
}
