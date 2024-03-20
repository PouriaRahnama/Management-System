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
