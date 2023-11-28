using MetalCollector.WebApi.Dtos;

namespace MetalCollector.WebApi.Services.Interfaces
{
    public interface IMetalArchivesClientService
    {
        Task<List<ArtistDto>> FetchArtists(string query);
        Task<ArtistDto> FetchArtistById(string id);
    }
}
