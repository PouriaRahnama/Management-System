
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

            //if (result.Succeeded)
            //{
            //    await userManager.AddToRoleAsync(user, SD.Admin);

            //    var temp1 = userManager.AddClaimsAsync(user, new Claim[]
            //    {
            //             new Claim(JwtClaimTypes.Name, user.FirstName + " " + user.LastName),
            //             new Claim(JwtClaimTypes.GivenName, user.FirstName),
            //             new Claim(JwtClaimTypes.FamilyName, user.LastName),
            //             new Claim(JwtClaimTypes.Email, user.Email),
            //             new Claim(JwtClaimTypes.Role, SD.Admin),
            //    }).Result;

            //}
        }
    }
}
