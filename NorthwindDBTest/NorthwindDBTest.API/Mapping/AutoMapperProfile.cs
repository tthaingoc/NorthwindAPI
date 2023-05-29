using AutoMapper;
using NorthwindDBTest.API.Models.Domain;
using NorthwindDBTest.API.Models.DTO;

namespace NorthwindDBTest.API.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<CreateProductDTO, Product>().ReverseMap();
            CreateMap<UpdateProductRequestDTO, Product>().ReverseMap();
        }

    }
}
