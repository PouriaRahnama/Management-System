namespace Management_System.Infrustructure.MappingProfile
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<CustomerDetailDto, Customer>().ReverseMap();
            CreateMap<AddCustomerDto, Customer>().ReverseMap();
            CreateMap<EditContactDto, Customer>().ReverseMap();
            CreateMap<SearchFilterContactDto, Customer>().ReverseMap();
        }
    }
}