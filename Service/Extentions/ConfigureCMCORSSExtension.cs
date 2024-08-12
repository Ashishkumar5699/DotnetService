using System.Reflection;

namespace Sonaar.Extentions
{
    public static class ConfigureCMCORSSExtension
    {
        //static readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public static IServiceCollection ConfigureCMCORSSetting(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddCors();
            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(
            //                      policy =>
            //                      {
            //                          policy.WithOrigins("http://localhost:5173");
            //                      });
            //});

                services. .AddApplicationPart(Assembly.Load(new AssemblyName("Sonaar.Service.CustomerDirectory")));

            return services;
        }
    }
}
        //}
