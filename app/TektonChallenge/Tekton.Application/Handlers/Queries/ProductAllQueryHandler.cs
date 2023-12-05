using AutoMapper;
using FluentValidation;
using MediatR;
using Tekton.Application.Interfaces.Repositories;
using Tekton.Domain.Dtos.Requests;
using Tekton.Domain.Dtos.Responses;

namespace Tekton.Application.Handlers.Queries
{
	public class ProductAllQueryHandler : IRequestHandler<ProductAllQueryRequest, Response<List<ProductAllQueryResponse>>>
	{
		private readonly IProductRepository _repository;
		private readonly IMapper _mapper;

		public ProductAllQueryHandler(IProductRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public Task<Response<List<ProductAllQueryResponse>>> Handle(ProductAllQueryRequest request, CancellationToken cancellationToken)
		{
			var response = new Response<List<ProductAllQueryResponse>>();
			var result = new List<ProductAllQueryResponse>();

			var resultGetProductsBy = _repository.GetProductsBy(q => q.StatusId == request.StatusId).ToList();

			if (resultGetProductsBy == null || resultGetProductsBy.Count <= 0)
			{
				response.ResultError("No se encontró el registro");
				return Task.FromResult(response);
			}
			
			result = _mapper.Map<List<ProductAllQueryResponse>>(resultGetProductsBy);

			response.ResultOk(result);
			return Task.FromResult(response);
		}
	}
}
