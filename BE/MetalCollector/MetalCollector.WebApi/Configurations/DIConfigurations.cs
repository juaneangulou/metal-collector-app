using MetalCollector.WebApi.Models;
using MetalCollector.WebApi.Repositories.Implementations;
using MetalCollector.WebApi.Repositories.Interfaces;
using MetalCollector.WebApi.Services;
using MetalCollector.WebApi.Services.Interfaces;

namespace MetalCollector.WebApi.Configurations
{
    public static class DIConfigurations
    {
        public static IServiceCollection AddDependenceInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IMetalArchivesClientService, MetalArchivesClientService>();
            services.AddScoped<IBaseRepository<Band>, BaseRepository<Band>>();
            services.AddScoped<IBaseRepository<Member>, BaseRepository<Member>>();
            services.AddScoped<IBaseRepository<Release>, BaseRepository<Release>>();
            services.AddScoped<IBaseRepository<Item>, BaseRepository<Item>>();
            services.AddScoped<IBaseRepository<Artist>, BaseRepository<Artist>>();
            services.AddScoped<IBaseRepository<Discography>, BaseRepository<Discography>>();
            services.AddScoped<IBaseRepository<Lineup>, BaseRepository<Lineup>>();
            services.AddScoped<IBaseRepository<Social>, BaseRepository<Social>>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IArtistService, ArtistService>();
            return services;
        }
    }
}
