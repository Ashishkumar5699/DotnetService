using Sonaar.Interface;
using Microsoft.EntityFrameworkCore;
using Sonaar.Domain.DataContexts;

namespace Sonaar.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt => {
                opt.UseSqlite(config.GetConnectionString("DefaultConnectionSQLite"));
                    //, b =>b.MigrationsAssembly());
                //opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<ITokenService, Services.TokenService>();
            return services;
        }
    }
}
