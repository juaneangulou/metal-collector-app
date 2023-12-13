using MetalCollector.WebApi.Dtos;
using MetalCollector.WebApi.Services.Interfaces;
using System.Text.Json;

namespace MetalCollector.WebApi.Services
{
    public class MetalArchivesClientService : IMetalArchivesClientService
    {
        private readonly HttpClient _httpClient;
    
        public MetalArchivesClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string ProcessQueryString(string query)
        {
            return query.Replace("  ", " ").Replace(" ", "*");
        }

        public async Task<List<ArtistMADto>> FetchArtists(string query)
        {
            string processedQuery = ProcessQueryString(query);
            var response = await _httpClient.GetAsync($"https://api.metal-map.com/v1/search/{processedQuery}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ArtistMADto>>(content);
            }

            return new List<ArtistMADto>();
        }

        public async Task<ArtistMADto> FetchArtistById(string id)
        {
            var response = await _httpClient.GetAsync($"https://api.metal-map.com/v1/bands/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var artists = JsonSerializer.Deserialize<List<ArtistMADto>>(content);
                return artists?[0];
            }

            return null;
        }

        public async Task<ArtistMADto> AddArtist(ArtistMADto artist)
        {
            throw new NotImplementedException();
        }
    }
}
