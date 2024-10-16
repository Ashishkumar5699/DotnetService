using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sonaar.Service.Identity.Repository;
using Sonaar.Service.Identity.Service;

namespace Sonaar.Service.Identity.Configure
{
    public static class ConfigureIdentity
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService>(provider => new TokenService(configuration));
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<IdentityService>();

            return services;
        }   
    }
}