using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sonaar.Service.CustomerDirectory.Repository;

namespace Sonaar.Service.CustomerDirectory.Configure;

public static class CustmerDirectoryConfiguration
{
    public static IServiceCollection ConfigureCustmorDirectory(this IServiceCollection services)
    {
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<Service.ClientService>();

        return services;
    }  
}
