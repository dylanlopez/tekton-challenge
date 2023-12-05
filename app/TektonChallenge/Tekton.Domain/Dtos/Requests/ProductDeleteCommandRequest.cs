using MediatR;
using Tekton.Domain.Dtos.Responses;

namespace Tekton.Domain.Dtos.Requests
{
	/// <summary>
	/// VallueObject para realizar las request al Handler <see cref="ProductDeleteCommandHandler"/>.
	/// </summary>
	public class ProductDeleteCommandRequest : IRequest<Response<int>>
	{
		/// <summary>
		/// El identificador único del producto al ejecutar <see cref="ProductDeleteCommandHandler"/>.
		/// </summary>
		public int ProductId { get; set; }
	}
}
