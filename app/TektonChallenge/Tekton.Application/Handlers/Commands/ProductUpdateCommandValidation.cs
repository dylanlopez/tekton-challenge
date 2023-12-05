using FluentValidation;
using Tekton.Domain.Dtos.Requests;

namespace Tekton.Application.Handlers.Commands
{
	public class ProductUpdateCommandValidation : AbstractValidator<ProductUpdateCommandRequest>
	{
		public ProductUpdateCommandValidation()
		{
			RuleFor(v => v.ProductId)
				.NotNull()
				.NotEmpty()
				.GreaterThan(0);

			RuleFor(v => v.Name)
				.NotNull()
				.NotEmpty();

			RuleFor(v => v.Stock)
				.NotNull()
				.NotEmpty()
				.GreaterThan(0);

			RuleFor(v => v.Price)
				.NotNull()
				.NotEmpty()
				.GreaterThan(0);
		}
	}
}
