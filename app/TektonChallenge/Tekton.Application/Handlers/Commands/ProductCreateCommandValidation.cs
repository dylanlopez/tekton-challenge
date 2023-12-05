using FluentValidation;
using Tekton.Domain.Dtos.Requests;

namespace Tekton.Application.Handlers.Commands
{
	public class ProductCreateCommandValidation : AbstractValidator<ProductCreateCommandRequest>
	{
		public ProductCreateCommandValidation()
		{
			RuleFor(v => v.Name)
				.NotNull()
				.NotEmpty();
			RuleFor(v => v.Name)
				.MaximumLength(50);

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
