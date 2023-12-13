using MetalCollector.WebApi.Dtos;

namespace MetalCollector.WebApi.Services.Interfaces
{
    public interface IArtistService
    {
        Task ArtistAdd(ArtistMADto artist);
        Task<ArtistMADto> GetArtistById(string id);
    }
}
