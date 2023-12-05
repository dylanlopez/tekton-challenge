using FluentValidation;
using Tekton.Domain.Dtos.Requests;

namespace Tekton.Application.Handlers.Commands
{
	public class ProductDeleteCommandValidation : AbstractValidator<ProductDeleteCommandRequest>
	{
		public ProductDeleteCommandValidation()
		{
			RuleFor(v => v.ProductId)
				.NotNull()
				.NotEmpty()
				.GreaterThan(0);
		}
	}
}
