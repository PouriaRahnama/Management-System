using Management_System.Models.Dtos;
using Management_System.Models.Entities;

namespace Management_System.Infrustructure.MappingProfile
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<AddProductDto, Product>().ReverseMap();
            CreateMap<EditProductDto, Product>().ReverseMap();
            CreateMap<SearchFilterProductDto, Product>().ReverseMap();
        }
    }
}
