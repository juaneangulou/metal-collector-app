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
            //var connectionString = Environment.GetEnvironmentVariable(MyVaccineLiterals.CONNECTION_STRING);
            var connectionString = configuration.GetConnectionString("DefaultConnection");
           // var connectionString = "Server=api-metalcollector.mymetalevents.com;Database=mymetalevents_metalcollectordb;User Id=metalcollector_admin;Password=Icaro88.21;Encrypt=False;TrustServerCertificate=True;";

            services.AddDbContext<MetalCollectorDbContext>(options =>
                        options.UseSqlServer(
                                    connectionString
                                    ), ServiceLifetime.Singleton
                        );
            return services;
        }
    }
}
