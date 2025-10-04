using Library.Application.Abstractions.Auth;
using Library.Application.Mapping;
using Library.Application.Services.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Library.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            //MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            //Services
            services.AddScoped<IAuthTokenService, AuthTokenService>();
            services.AddAutoMapper(cfg => { }, typeof(BookProfile).Assembly);


            return services;
        }
    }
}
