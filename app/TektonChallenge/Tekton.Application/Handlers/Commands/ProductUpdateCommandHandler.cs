using AutoMapper;
using FluentValidation;
using MediatR;
using Tekton.Application.Interfaces.Repositories;
using Tekton.Domain.Dtos.Requests;
using Tekton.Domain.Dtos.Responses;
using Tekton.Domain.Entities;

namespace Tekton.Application.Handlers.Commands
{
	public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommandRequest, Response<int>>
	{
		private readonly IProductRepository _repository;
		private readonly IMapper _mapper;
		private readonly IValidator<ProductUpdateCommandRequest> _validator;

		public ProductUpdateCommandHandler(IProductRepository repository, IMapper mapper, IValidator<ProductUpdateCommandRequest> validator)
		{
			_repository = repository;
			_mapper = mapper;
			_validator = validator;
		}

		public async Task<Response<int>> Handle(ProductUpdateCommandRequest request, CancellationToken cancellationToken)
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

				var entity = _mapper.Map<Product>(request);
				var result = await _repository.Update(entity);
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
