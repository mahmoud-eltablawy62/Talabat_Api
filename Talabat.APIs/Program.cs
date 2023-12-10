using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Extensions;
using Talabat.APIs.MiddleWares;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            

            builder.Services.AddControllers();

            builder.Services.SwaggerServ();

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<UserContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

         
            builder.Services.AddServices();

            builder.Services.AddSingleton<IConnectionMultiplexer>(s =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return  ConnectionMultiplexer.Connect(connection);
            });


            builder.Services.AddIdentitiyService(builder.Configuration);

            #endregion

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleWares>();

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;  
            
            var _dbContext = services.GetRequiredService<StoreContext>(); 

            var _IdentityContext = services.GetRequiredService<UserContext>();  
            

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync(); 
                await StoreContextSeed.SeedAsync(_dbContext);
                await _IdentityContext.Database.MigrateAsync();
                var _user_manager = services.GetRequiredService<UserManager<Users>>();
                await UserContextSeed.UserSeedAsync(_user_manager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex,"an error has been occured during apply the migration ");
            }

            #region Configure Kestrel Middelwears
           
            if (app.Environment.IsDevelopment())
            {
                app.SwaggerMiddleWares();
            }
            app.UseStatusCodePagesWithRedirects("/error/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
         
            app.MapControllers();


            app.UseAuthentication();

            app.UseAuthorization();
            

            #endregion

            app.Run();
        }
    }
}