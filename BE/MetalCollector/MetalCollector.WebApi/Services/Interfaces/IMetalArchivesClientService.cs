using MetalCollector.WebApi.Dtos;

namespace MetalCollector.WebApi.Services.Interfaces
{
    public interface IMetalArchivesClientService
    {
        Task<List<ArtistMADto>> FetchArtists(string query);
        Task<ArtistMADto> FetchArtistById(string id);
        Task<ArtistMADto> AddArtist(ArtistMADto artist);
    }
}
