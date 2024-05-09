namespace Management_System.Services
{
    public interface IAccountService
    {
        Task<StatusResultDto> Add(AddAccountDto addAccountDto);
        Task<StatusResultDto> Edit(EditAccountDto editAccountDto);
        Task<StatusResultDto> SignIn(LoginAccountDto loginAccountDto);
        StatusResultDto CheckSignIn(ClaimsPrincipal principal);
        Task SignOut();
        Task<IEnumerable<AccountDto>> GetAllUsers();
        Task<IEnumerable<IdentityRole>> GetAllRoles();
        Task<EditAccountDto> GetUserById(string Id);
        Task<bool> DeleteUser(string Id);
    }

    [EasyDi(ServiceLifetime.Scoped)]
    [Display(Name = "این قسمت مرتبط با مدیریت کاربران می باشد")]
    public class AccountService : IAccountService
    {
        #region Constructor
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IHttpContextAccessor context;
        private readonly IMapper mapper;

        public AccountService(IMapper Mapper, UserManager<IdentityUser> UserManager, SignInManager<IdentityUser> SignInManager, IHttpContextAccessor Context, RoleManager<IdentityRole> roleManager)
        {
            mapper = Mapper;
            userManager = UserManager;
            signInManager = SignInManager;
            context = Context;
            this.roleManager = roleManager;
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

        public async Task<IEnumerable<IdentityRole>> GetAllRoles()
        {
            return await roleManager.Roles.ToListAsync();
        }

        public async Task<EditAccountDto> GetUserById(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            var roles = await userManager.GetRolesAsync(user);
            EditAccountDto editUser = new EditAccountDto
            {
                Id = user.Id,
                Email = user.Email,
                RoleName = null,
                UserName = user.UserName
            };
            foreach (var role in roles)
            {
                editUser.RoleName = role + ",";
            }

            return editUser;
        }

        public async Task<bool> DeleteUser(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            var roles = await userManager.GetRolesAsync(user);
            var cliams = await userManager.GetClaimsAsync(user);

            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    await userManager.RemoveFromRolesAsync(user, roles);
                    await userManager.RemoveClaimsAsync(user, cliams);
                }
                return true;
            }

            return false;
        }

        public async Task<StatusResultDto> Edit(EditAccountDto editAccountDto)
        {
            var user = await userManager.FindByIdAsync(editAccountDto.Id);
            var userRoles = await userManager.GetRolesAsync(user);
            var userClaims = await userManager.GetClaimsAsync(user);

            user.Email = editAccountDto.Email;
            user.UserName = editAccountDto.UserName;

            var resultUpdate = await userManager.UpdateAsync(user);

            if (resultUpdate.Succeeded)
            {
                var resultRemoveRoles = await userManager.RemoveFromRolesAsync(user, userRoles);
                if (resultRemoveRoles.Succeeded)
                    await userManager.AddToRoleAsync(user, editAccountDto.RoleName);

                var resultRemoveClaims = await userManager.RemoveClaimsAsync(user, userClaims);
                if (resultRemoveClaims.Succeeded)
                    await userManager.AddClaimsAsync(user, new Claim[]
                    {
                    new Claim("Name", editAccountDto.UserName!),
                    new Claim("Role", editAccountDto.RoleName),
                    new Claim("Email", editAccountDto.Email!)
                    });

                return StatusResultDto.Success;
            }

            return StatusResultDto.Failure;
        }
    }
}
