using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Extensions
{
    public static class UserManagerExtentions
    {
        public static async Task<Users> FindAddressAsync (this UserManager<Users> manager , ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);
            var User = await manager.Users.Include(u => u.Adress).FirstOrDefaultAsync(u => u.Email == email);
            return User;
        }
    }
}
