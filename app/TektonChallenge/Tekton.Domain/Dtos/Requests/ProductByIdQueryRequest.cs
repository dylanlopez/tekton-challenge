using MediatR;
using Tekton.Domain.Dtos.Responses;

namespace Tekton.Domain.Dtos.Requests
{
	/// <summary>
	/// VallueObject para realizar las request al Handler <see cref="ProductByIdQueryHandler"/>.
	/// </summary>
	public class ProductByIdQueryRequest : IRequest<Response<ProductByIdQueryResponse>>
	{
		/// <summary>
		/// El identificador único del producto al ejecutar <see cref="ProductByIdQueryHandler"/>.
		/// </summary>
		public int ProductId { get; set; }
	}
}
