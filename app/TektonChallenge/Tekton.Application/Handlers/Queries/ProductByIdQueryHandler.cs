using AutoMapper;
using FluentValidation;
using MediatR;
using Tekton.Application.Interfaces.Repositories;
using Tekton.Domain.Dtos.Requests;
using Tekton.Domain.Dtos.Responses;

namespace Tekton.Application.Handlers.Queries
{
	public class ProductByIdQueryHandler : IRequestHandler<ProductByIdQueryRequest, Response<ProductByIdQueryResponse>>
	{
		private readonly IProductRepository _repository;
		private readonly IMapper _mapper;
		private readonly IValidator<ProductByIdQueryRequest> _validator;

		public ProductByIdQueryHandler(IProductRepository repository, IMapper mapper, IValidator<ProductByIdQueryRequest> validator)
		{
			_repository = repository;
			_mapper = mapper;
			_validator = validator;
		}

		public async Task<Response<ProductByIdQueryResponse>> Handle(ProductByIdQueryRequest request, CancellationToken cancellationToken)
		{
			var response = new Response<ProductByIdQueryResponse>();
			var result = new ProductByIdQueryResponse();
			try
			{
				var validation = await _validator.ValidateAsync(request, cancellationToken);
				if (!validation.IsValid)
				{
					var errorMessages = string.Join("; ", validation.Errors.Select(e => e.ErrorMessage));
					response.ResultError($"Errores de validación: {errorMessages}");
					return response;
				}

				var resultGetProductsBy = _repository.GetProductsBy(q => q.Id == request.ProductId).ToList();
				if (resultGetProductsBy == null || resultGetProductsBy.Count <= 0)
				{
					response.ResultError("No se encontró el registro");
					return response;
				}

				var entity = resultGetProductsBy.FirstOrDefault();
				result = _mapper.Map<ProductByIdQueryResponse>(entity);

				response.ResultOk(result);
			}
			catch (Exception ex)
			{
				response.State = 500;
				response.Message = $"Error interno en el servidor: {ex.Message}";
			}
			return response;
		}
	}
}
