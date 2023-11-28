using MetalCollector.WebApi.Services;
using MetalCollector.WebApi.Services.Interfaces;

namespace MetalCollector.WebApi.Configurations
{
    public static class DIConfigurations
    {
        public static IServiceCollection AddDependenceInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IMetalArchivesClientService, MetalArchivesClientService>();

            return services;
        }
    }
}
