using MediatR;
using Tekton.Domain.Dtos.Responses;

namespace Tekton.Domain.Dtos.Requests
{
	/// <summary>
	/// VallueObject para realizar las request al Handler <see cref="ProductAllQueryHandler"/>.
	/// </summary>
	public class ProductAllQueryRequest : IRequest<Response<List<ProductAllQueryResponse>>>
	{
		/// <summary>
		/// El identificador del estado del producto al ejecutar <see cref="ProductAllQueryHandler"/>.
		/// </summary>
		public int StatusId { get; set; }
	}
}
