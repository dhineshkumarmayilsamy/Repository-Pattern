using AutoMapper;
using Model.DomainModel;
using Model.Dto;

namespace Service
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
