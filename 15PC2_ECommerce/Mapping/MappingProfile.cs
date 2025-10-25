using _15PC2_ECommerce.DTOs.CategoryDTOs;
using _15PC2_ECommerce.DTOs.CustomerDTOs;
using _15PC2_ECommerce.DTOs.LogDTOs;
using _15PC2_ECommerce.DTOs.OrderDTOs;
using _15PC2_ECommerce.DTOs.ProductDTOs;
using _15PC2_ECommerce.Entities;
using AutoMapper;

namespace _15PC2_ECommerce.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, ResultCategoryDTO>().ReverseMap();
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
            CreateMap<Category, GetCategoryDTO>().ReverseMap();
            CreateMap<GetCategoryDTO, UpdateCategoryDTO>();

            CreateMap<Customer, ResultCustomerDTO>().ReverseMap();
            CreateMap<Customer, CreateCustomerDTO>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDTO>().ReverseMap();
            CreateMap<Customer, GetCustomerDTO>().ReverseMap();
            CreateMap<GetCustomerDTO, UpdateCustomerDTO>().ReverseMap();

            CreateMap<Order, ResultOrderDTO>().ReverseMap();
            CreateMap<Order, CreateOrderDTO>().ReverseMap();
            CreateMap<Order, UpdateOrderDTO>().ReverseMap();
            CreateMap<Order, GetOrderDTO>().ReverseMap();
            CreateMap<GetOrderDTO, UpdateOrderDTO>().ReverseMap();         

            CreateMap<Product, ResultProductDTO>().ReverseMap();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<Product, GetProductDTO>().ReverseMap();
            CreateMap<GetProductDTO, UpdateProductDTO>().ReverseMap();

            CreateMap<Log, ResultLogDTO>().ReverseMap();
        }
    }
}
