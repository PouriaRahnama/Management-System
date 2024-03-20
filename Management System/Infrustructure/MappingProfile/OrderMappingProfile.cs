namespace Management_System.Infrustructure.MappingProfile
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderDto>();
            CreateMap<AddOrderDto, Order>().ReverseMap();
            CreateMap<SearchFilterOrderDto, Order>().ReverseMap();
        }
    }
}
