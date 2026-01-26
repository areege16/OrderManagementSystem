using AutoMapper;
using OrderManagementSystem.Application.DTOs.Product.Admin;
using OrderManagementSystem.Application.DTOs.Product.Customer;
using OrderManagementSystem.Domain.Models;

namespace OrderManagementSystem.Application.AutoMapperProfile
{
    public class OrderManagementSystem_Profiler : Profile
    {
        public OrderManagementSystem_Profiler()
        {
            #region Product
            CreateMap<CreateProductDto, Product>();

            CreateMap<UpdateProductDto, Product>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore())
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Product, GetAllProductsDto>();

            CreateMap<Product, GetProductDetailsDto>();
            #endregion
        }
    }
}
