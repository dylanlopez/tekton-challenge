using AutoMapper;
using FluentValidation;
using MediatR;
using Tekton.Application.Interfaces.Repositories;
using Tekton.Domain.Dtos.Requests;
using Tekton.Domain.Dtos.Responses;

namespace Tekton.Application.Handlers.Commands
{
	public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommandRequest, Response<int>>
	{
		private readonly IProductRepository _repository;
		private readonly IMapper _mapper;
		private readonly IValidator<ProductDeleteCommandRequest> _validator;

		public ProductDeleteCommandHandler(IProductRepository repository, IMapper mapper, IValidator<ProductDeleteCommandRequest> validator)
		{
			_repository = repository;
			_mapper = mapper;
			_validator = validator;
		}

		public async Task<Response<int>> Handle(ProductDeleteCommandRequest request, CancellationToken cancellationToken)
		{
			var response = new Response<int>();
			try
			{
				var validation = await _validator.ValidateAsync(request, cancellationToken);
				if (!validation.IsValid)
				{
					var errorMessages = string.Join("; ", validation.Errors.Select(e => e.ErrorMessage));
					response.ResultError($"Errores de validación: { errorMessages }");
					return response;
				}

				var resultGetProductsBy = _repository.GetProductsBy(q => q.Id == request.ProductId).ToList();
				if (resultGetProductsBy == null || resultGetProductsBy.Count <= 0)
				{
					response.ResultError("Data Not Found");
					return response;
				}

				var entity = resultGetProductsBy.FirstOrDefault();
				var result = await _repository.Delete(entity);
				response.ResultOk(result);
			}
			catch (Exception ex)
			{
				response.State = 500;
				response.Message = $"Error interno en el servidor: { ex.Message }";
			}
			return response;
		}
	}
}
