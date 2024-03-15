using Management_System.Models.Dtos;
using System.Security.Claims;

namespace Management_System.Services
{
    public interface IAccountService
    {
        Task<StatusResultDto> Add(AddAccountDto addAccountDto);
        Task<StatusResultDto> SignIn(LoginAccountDto loginAccountDto);
        StatusResultDto CheckSignIn(ClaimsPrincipal principal);
        Task SignOut();
    }

    [EasyDi(ServiceLifetime.Scoped)]
    [Display(Name = "این قسمت مرتبط با مدیریت کاربران می باشد")]
    public class AccountService : IAccountService
    {
        #region Constructor
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IHttpContextAccessor context;
        private readonly IMapper mapper;

        public AccountService(IMapper Mapper, UserManager<IdentityUser> UserManager, SignInManager<IdentityUser> SignInManager, IHttpContextAccessor Context)
        {
            mapper = Mapper;
            userManager = UserManager;
            signInManager = SignInManager;
            context = Context;
        }
        #endregion

        public async Task<StatusResultDto> Add(AddAccountDto addAccountDto)
        {
            var newAccount = mapper.Map<IdentityUser>(addAccountDto);
            var account = await userManager.CreateAsync(newAccount, addAccountDto.Password);
            if (!account.Succeeded) return StatusResultDto.Failure;

            return StatusResultDto.Success;
        }
        public async Task<StatusResultDto> SignIn(LoginAccountDto loginAccountDto)
        {
            var signIn = await signInManager.PasswordSignInAsync(loginAccountDto.UserName, loginAccountDto.Password,
                loginAccountDto.RememberMe, true);
            if (signIn.IsLockedOut)
                return StatusResultDto.Success;
            if (!signIn.Succeeded)
                return StatusResultDto.Failure;

            return StatusResultDto.Success;
        }
        public StatusResultDto CheckSignIn(ClaimsPrincipal principal)
        {
            var status = signInManager.IsSignedIn(principal);
            if (!status) return StatusResultDto.NotEntred;
            return StatusResultDto.Entred;
        }
        public async Task SignOut()
        {
            await signInManager.SignOutAsync();
        }
    }
}
