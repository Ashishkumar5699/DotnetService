using System.Reflection;
using Sonaar.Domain.Mapper;
using Sonaar.Extentions;
using Sonaar.Service.QuotationManagement.Configure;

namespace Sonaar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //builder.Services.AddControllers();
            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.ConfigureCMCORSSetting(builder.Configuration);

            builder.Services.AddLibrarySettings();

            builder.Services.AddAutoSetting();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173"));

            app.MapControllers();

            app.Run();
        }

    }

    public static class LibraryRegister
    {
        public static IServiceCollection AddLibrarySettings(this IServiceCollection services)
        {
            services.ConfigureQuotationServices();

            return services;
        }
    }
}