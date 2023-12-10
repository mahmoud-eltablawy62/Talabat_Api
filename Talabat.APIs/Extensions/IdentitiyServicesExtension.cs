using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.APIs.Extensions
{
    public  static class IdentitiyServicesExtension 
    {
        public static IServiceCollection AddIdentitiyService(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddScoped(typeof(IAuthServer), typeof(AuthServer));
            services.AddIdentity<Users, IdentityRole>(options => {
            }).AddEntityFrameworkStores<UserContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            } )
                .AddJwtBearer(options =>
                {
                   

                    options.TokenValidationParameters = new TokenValidationParameters()
                        {
                ValidateAudience = true,
                ValidAudience = configuration["JWT:ValidAudience"],
                ValidateIssuer = true,
                ValidIssuer = configuration["Jwt:ValidIssuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromDays(double.Parse(configuration["JWT:expireDay"]))
            };
        });
            return services;
        }
    }
}
