using FluentValidation;
using Tekton.Domain.Dtos.Requests;

namespace Tekton.Application.Handlers.Commands
{
	public class ProductUpdateStockCommandValidation : AbstractValidator<ProductUpdateStockCommandRequest>
	{
		public ProductUpdateStockCommandValidation()
		{
			RuleFor(v => v.ProductId)
				.NotNull()
				.NotEmpty()
				.GreaterThan(0);

			RuleFor(v => v.Stock)
				.NotNull()
				.NotEmpty()
				.GreaterThan(0);
		}
	}
}
