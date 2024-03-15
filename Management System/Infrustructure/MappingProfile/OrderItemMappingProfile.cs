using Management_System.Models.Dtos;
using Management_System.Models.Entities;

namespace Management_System.Infrustructure.MappingProfile
{
    public class OrderItemMappingProfile : Profile
    {
        public OrderItemMappingProfile()
        {
            CreateMap<OrderItemDto, OrderItem>().ReverseMap();
            CreateMap<AddItemDto, OrderItem>().ReverseMap();
            CreateMap<EditItemDto, OrderItem>().ReverseMap();
            CreateMap<Order, OrderDetailDto>().ReverseMap();
        }
    }
}
