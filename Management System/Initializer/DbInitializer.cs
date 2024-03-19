
using Management_System.Models.Dtos;
using System.Security.Claims;
using System.Security.Principal;

namespace Management_System.Initializer
{
    public class DbInitializer : IDbinitializer
    {
        #region Fileds

        private UserManager<IdentityUser> userManager;

        public DbInitializer(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        #endregion
        public async Task Initialize()
        {

            IdentityUser user = new IdentityUser
            {
                UserName = "pouriarahnama",
                Email = "pouria.rahnama78@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+989367394994"
            };
            var result = await userManager.CreateAsync(user, "Pouria@2024");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
                await userManager.AddClaimsAsync(user, new Claim[]
                {
                    new Claim("Name",user.UserName!),
                    new Claim("Role","Admin"),
                    new Claim("Email" ,user.Email!),
                });

            }
        }
    }
}
