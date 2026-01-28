using AutoMapper;
using OrderManagementSystem.Application.DTOs.Order.Admin;
using OrderManagementSystem.Application.DTOs.Order.Customer;
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

            CreateMap<Product, ProductSummaryDto>();

            CreateMap<Product, ProductDetailsDto>();
            #endregion

            #region Order
            CreateMap<Order, OrderDetailsDto>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems)); ;

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.Discount));

            CreateMap<Order, AdminOrderSummaryDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.ApplicationUser.Name));

            CreateMap<Order, CustomerOrderSummaryDto>();
            #endregion
        }
    }
}