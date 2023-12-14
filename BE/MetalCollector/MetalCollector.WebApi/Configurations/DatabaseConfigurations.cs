using MetalCollector.WebApi.Services.Interfaces;
using MetalCollector.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using MetalCollector.WebApi.Models;
using Microsoft.Extensions.Configuration;

namespace MetalCollector.WebApi.Configurations
{
    public static class DatabaseConfigurations 
    {

        public static IServiceCollection SetDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
              var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<MetalCollectorDbContext>(options =>
                        options.UseSqlServer(
                                    connectionString
                                    ), ServiceLifetime.Singleton
                        );
            return services;
        }
    }
}
