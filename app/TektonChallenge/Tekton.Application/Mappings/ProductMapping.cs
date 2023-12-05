using AutoMapper;
using Tekton.Domain.Dtos.Requests;
using Tekton.Domain.Dtos.Responses;
using Tekton.Domain.Entities;

namespace Tekton.Application.Mappings
{
	public class ProductMapping : Profile
	{
		public ProductMapping()
		{
			CreateMap<Product, ProductByIdQueryResponse > ()
				.ForMember(dest => dest.ProductId,
					opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.StatusName,
					opt => opt.MapFrom(src => src.Status.Name))
				.AfterMap((src, dest) =>
				{
					dest.FinalPrice = src.Price * ((100 - src.Discount)/100);
				});
			CreateMap<Product, ProductAllQueryResponse>()
				.ForMember(dest => dest.ProductId,
					opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.StatusName,
					opt => opt.MapFrom(src => src.Status.Name))
				.AfterMap((src, dest) =>
				{
					dest.FinalPrice = src.Price * ((100 - src.Discount) / 100);
				});
			CreateMap<ProductCreateCommandRequest, Product>();
			CreateMap<ProductUpdateCommandRequest, Product>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId));
		}
	}
}
