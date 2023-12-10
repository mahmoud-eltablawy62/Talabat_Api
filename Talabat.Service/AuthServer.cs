using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.Service
{
    public class AuthServer : IAuthServer
    {
        private readonly IConfiguration _Config;
        public AuthServer(IConfiguration Config)
        {
            _Config = Config;   
        }
        public async Task<string> CreateTokenAsync(Users userApp  , UserManager<Users> userManager)
        {
           
            //// private claims 
            var _Claims = new List<Claim>()
            {
            new Claim (ClaimTypes.GivenName , userApp.UserName), ////تحديد اسم اليوزر وتخزينه 

            new Claim (ClaimTypes.Email , userApp.Email)  //////   تحديد الايميل وتخزينه
                                                          
            };

            var User_Role = await userManager.GetRolesAsync(userApp); 
                        //////تحديد كل شخص هل هو يوزر ولا ادمن 
             foreach(var role in User_Role)
            {
                _Claims.Add(new Claim(ClaimTypes.Role , role)); ////// هتعمل لوب علشان تضيف للي داخل جواه الكلامز هو يوزر ولا ادمن 
            }           

             /////// Signtuare Part

            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config["JWT:SecretKey"]));
            var token = new JwtSecurityToken(
                audience : _Config["JWT:ValidAudience"],
                issuer : _Config["Jwt:ValidIssuer"],
                expires : DateTime.UtcNow.AddDays(double.Parse(_Config["JWT:expireDay"])),
                claims : _Claims,
                signingCredentials : new SigningCredentials(AuthKey,SecurityAlgorithms.HmacSha256Signature)
                );         
                return new JwtSecurityTokenHandler().WriteToken(token);
        }  
    }
}
