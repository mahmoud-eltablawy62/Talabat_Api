using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;
using Talabat.Repository.Identity.Oreder_Aggregate;

namespace Talabat.APIs.Controllers
{
 
    public class AccountController : BaseController
    {
        private readonly UserManager<Users> _UserManager;
        private readonly SignInManager<Users> _SignInManager;
        private readonly IAuthServer _authServer;
        private readonly IMapper _Map;

        public AccountController (UserManager<Users> UserManager  , SignInManager<Users> SignInManager ,
            IAuthServer authServer , IMapper Map)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _authServer = authServer;   
            _Map = Map; 
        }

        [HttpPost("Login")]
        public async Task <ActionResult<UserDto>> Login (LoginDto Model)
        {
            var user = await _UserManager.FindByEmailAsync(Model.Email);    
            if (user == null)
            {return Unauthorized(new ApiResponse(401));}
            var pass = await _SignInManager.CheckPasswordSignInAsync(user, Model.Password, false);
            if (pass.Succeeded is false){return Unauthorized(new ApiResponse(401));}
            return Ok(new UserDto()
            {
                 Display  = user.DisplayName ,
                 Email = user.Email,
                 Token = await _authServer.CreateTokenAsync(user , _UserManager)
            });}

        [HttpPost("Register")]
        public async Task <ActionResult<UserDto>> Register(SignDto Model)
        {

            if (checkedEmail(Model.Email).Result.Value)
            {
                return BadRequest(new ApiValidation() 
                { Errors = new string[] { "this email is exist" } });
            }

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
            Token = await _authServer.CreateTokenAsync(user, _UserManager)
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _UserManager.FindByEmailAsync(email);
            return Ok(new UserDto()
            {
                Display = user.DisplayName,
                Email = user.Email,
                Token = await _authServer.CreateTokenAsync(user, _UserManager)
            });
        }
        [Authorize]
        [HttpGet("Address")]   
        public async Task <ActionResult<AddressDto>> GetUserAddress()
        {          
            var user = await _UserManager.FindAddressAsync(User);

            var Address = _Map.Map<AddressDto>(user.Adress);

            return Ok(Address);
        }
        


        [Authorize]
        [HttpPut("UpdateAddress")]
        public async Task<ActionResult<AddressDto>> UpdateAdress(AddressDto Updated)
        {
            var address = _Map.Map<AddressDto, Adress>(Updated);
            var user =await _UserManager.FindAddressAsync(User);
            user.Adress = address;
            address.Id = user.Adress.Id;
            
            var result = await _UserManager.UpdateAsync(user);
            if(!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }

            return Ok(Updated);
        }


        [HttpGet("CkeckEmail")]
        public async Task<ActionResult<bool>> checkedEmail(string Email)
        {
            return await _UserManager.FindByEmailAsync(Email) is not null;
        }
    }
}
