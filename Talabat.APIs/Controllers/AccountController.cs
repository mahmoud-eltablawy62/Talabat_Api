using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Controllers
{
 
    public class AccountController : BaseController
    {
        private readonly UserManager<Users> _UserManager;
        private readonly SignInManager<Users> _SignInManager;

        public AccountController ( UserManager<Users> UserManager  , SignInManager<Users> SignInManager )
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
        }

        [HttpPost("Login")]
        public async Task <ActionResult<UserDto>> Login (LoginDto Model)
        {
            var user = await _UserManager.FindByEmailAsync(Model.Email);    
            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }
            var pass = await _SignInManager.CheckPasswordSignInAsync(user, Model.Password, false);

            if (pass.Succeeded is false)
            {
                return Unauthorized(new ApiResponse(401));
            }

            return Ok(new UserDto()
            {
                 Display  = user.DisplayName ,
                 Email = user.Email,
                 Token = "okay",
            }
            );
        }

        [HttpPost("Register")]
        public async Task <ActionResult<UserDto>> Register(SignDto Model)
        {
            var user = new Users()
            {
               DisplayName = Model.DisplayName,
               Email = Model.Email,
               UserName = Model.Email.Split('@')[0],
               PhoneNumber = Model.PhoneNumber 
            };
            var result = await _UserManager.CreateAsync(user , Model.Password);   
            if (result.Succeeded is false) { return BadRequest(new ApiResponse(400));}
            return Ok(new UserDto() { 
            Display = user.DisplayName ,
            Email = user.Email,
            Token = "okay"
            });

        }




    }
}
