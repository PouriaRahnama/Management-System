using Management_System.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Management_System.Services
{
    public interface IAccountService
    {
        Task<StatusResultDto> Add(AddAccountDto addAccountDto);
        Task<StatusResultDto> SignIn(LoginAccountDto loginAccountDto);
        StatusResultDto CheckSignIn(ClaimsPrincipal principal);
        Task SignOut();
        Task<IEnumerable<AccountDto>> GetAllUsers();
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
            if (account.Succeeded)
            {
                if (!string.IsNullOrEmpty(addAccountDto.RoleName))
                {
                    var resultRole = userManager.AddToRoleAsync(newAccount, addAccountDto.RoleName).GetAwaiter().GetResult();
                    if (resultRole.Succeeded)
                    {
                        await userManager.AddClaimsAsync(newAccount, new Claim[]
                        {
                            new Claim("Name",newAccount.UserName!),
                            new Claim("Role",addAccountDto.RoleName),
                            new Claim("Email" ,newAccount.Email!),

                        });
                    }
                }
                return StatusResultDto.Success;
            }

            return StatusResultDto.Failure;


        }

        public async Task<StatusResultDto> SignIn(LoginAccountDto loginAccountDto)
        {
            var signIn = await signInManager.PasswordSignInAsync(loginAccountDto.UserName, loginAccountDto.Password,
                loginAccountDto.RememberMe, false);
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


        [Display(Name = "لیست کلیه کاربران")]
        public async Task<IEnumerable<AccountDto>> GetAllUsers()
        {
            var userWithRoles = new List<AccountDto>();
            var users = await userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);

                AccountDto uInfo = new()
                {
                    UserName = user.UserName!,
                    Email = user.Email!,
                    Id = user.Id
                };
                foreach (var role in roles)
                {
                    uInfo.RoleName += $" {role}";
                }
                userWithRoles.Add(uInfo);
            }
            return userWithRoles;
        }
    }
}
