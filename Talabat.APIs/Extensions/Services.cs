using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core;
using Talabat.Core.Repostries.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository;
using Talabat.Service;

namespace Talabat.APIs.Extensions
{
    public static class Services 
    {
        public static IServiceCollection AddServices( this IServiceCollection Services) {
            Services.AddScoped(typeof(IGenaricRepo<>), typeof(GenaricRepo<>));
            Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            Services.AddScoped(typeof(IOrderServ), typeof(ServiceOrder));
            Services.AddScoped(typeof(IProductServ), typeof(ProductService));
            Services.AddScoped(typeof(IBasketRepo), typeof(BasketRepo));
            Services.AddAutoMapper(typeof(MappingProfile));

            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                         .SelectMany(p => p.Value.Errors)
                                                         .Select(E => E.ErrorMessage)
                                                         .ToArray();
                    var Validation = new ApiValidation()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(Validation);
                };
            });
            return Services;
        }

    }
}
