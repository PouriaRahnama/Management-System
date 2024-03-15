using AutoMapper;
using Management_System.Models.Dtos;
using Management_System.Models.Entities;

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
            CreateMap<PatchCustomerDto, Customer>().ReverseMap();
            CreateMap<SearchFilterContactDto, Customer>().ReverseMap();
            CreateMap<ContactsofCustomer, Customer>().ReverseMap();
        }
    }
}