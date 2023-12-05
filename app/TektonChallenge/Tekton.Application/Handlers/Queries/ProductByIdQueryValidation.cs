using FluentValidation;
using Tekton.Domain.Dtos.Requests;

namespace Tekton.Application.Handlers.Queries
{
	public class ProductByIdQueryValidation : AbstractValidator<ProductByIdQueryRequest>
	{
		public ProductByIdQueryValidation()
		{
			RuleFor(v => v.ProductId)
				.NotNull()
				.NotEmpty()
				.GreaterThan(0);
		}
	}
}
