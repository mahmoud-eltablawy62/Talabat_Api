namespace Talabat.APIs.Extensions
{
    public static class SwaggerServices
    {
        public static IServiceCollection SwaggerServ(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();

            return Services;    
        }

        public static void SwaggerMiddleWares(this WebApplication app)
        {
                app.UseSwagger();
                app.UseSwaggerUI();
        }
    }
}
