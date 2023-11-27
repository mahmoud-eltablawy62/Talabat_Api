using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class UserContextSeed
    {
        public static async Task UserSeedAsync(UserManager<Users> _user)
        {
            if (_user.Users.Count() == 0)
            {
                var user = new Users()
                {
                    DisplayName = "mahmoud",
                    Email = "mahmoudeltablawy702@gmail.com",
                    UserName = "Eltablawy",
                    PhoneNumber = "01554847412"
                };
                await _user.CreateAsync(user, "mahM2#");
            }
        }
    }
}
