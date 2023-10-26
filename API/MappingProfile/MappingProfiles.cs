using AutoMapper;
using DAL.Model;
using DLL.Dto;

namespace API.MappingProfile
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();

        }
    }
}
