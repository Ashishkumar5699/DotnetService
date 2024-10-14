using Microsoft.Extensions.DependencyInjection;
using Sonaar.Service.QuotationManagement.Repository;
using Sonaar.Service.QuotationManagement.Service;

namespace Sonaar.Service.QuotationManagement.Configure
{
    public static class QuotationConfiguration
	{
        public static IServiceCollection ConfigureQuotationServices(this IServiceCollection services)
        {
            services.AddScoped<IQuotationRepository, QuotationRepository>();
            services.AddScoped<QuotationService>();

            return services;
        }
    }
}

