using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helpers;
using Talabat.APIs.MiddleWares;
using Talabat.Core.Repostries.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;

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
              

            builder.Services.AddServices();

            #endregion


            


            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleWares>();

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;  
            
            var _dbContext = services.GetRequiredService<StoreContext>();
            

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync(); 
                await StoreContextSeed.SeedAsync(_dbContext); 
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
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}