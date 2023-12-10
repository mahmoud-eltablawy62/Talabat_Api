using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.MiddleWares
{
    public class ExceptionMiddleWares
    {
        private readonly RequestDelegate Next;
        private readonly ILogger<ExceptionMiddleWares> Logger;
        private readonly IHostEnvironment  env;
        public ExceptionMiddleWares(RequestDelegate Next , ILogger<ExceptionMiddleWares> Logger, IHostEnvironment env)
        {
            this.Next = Next;
            this.Logger = Logger;
            this.env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try { 

              await Next.Invoke(context);

            }
            catch(Exception ex) { 
            
                Logger.LogError(ex, ex.Message); 

                context.Response.ContentType = "application/json";
                context.Response.StatusCode =  (int) HttpStatusCode.InternalServerError;
                var response = env.IsDevelopment() ?
                    new ApiExcaptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) :
                    new ApiExcaptionResponse((int)HttpStatusCode.InternalServerError);

                var camalCase = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };  

                var json = JsonSerializer.Serialize(response , camalCase);
                await context.Response.WriteAsync(json);

            }
        }

    }
}
