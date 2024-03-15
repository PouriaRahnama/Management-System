using Management_System.Models.Dtos;

namespace Management_System.Infrustructure.MappingProfile
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<AddAccountDto, IdentityUser>().ReverseMap();
        }
    }
}
