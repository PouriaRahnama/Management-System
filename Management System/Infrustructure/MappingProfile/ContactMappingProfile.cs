using Management_System.Models.Dtos;
using Management_System.Models.Entities;

namespace Management_System.Infrustructure.MappingProfile
{
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile()
        {
            CreateMap<ContactDto, Contact>().ReverseMap();
            CreateMap<AddContactDto, Contact>().ReverseMap();
            CreateMap<AddContactCustomerDto, AddContactDto>().ReverseMap();
            CreateMap<EditContactDto, Contact>().ReverseMap();
            CreateMap<PatchContactDto, Contact>().ReverseMap();
            CreateMap<SearchFilterContactDto, Contact>().ReverseMap();
            CreateMap<ContactCustomerDto, Contact>().ReverseMap();
        }
    }
}
