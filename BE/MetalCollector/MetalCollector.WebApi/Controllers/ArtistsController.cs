using MetalCollector.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetalCollector.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IMetalArchivesClientService _metalArchivesClientService;
        public ArtistsController(IMetalArchivesClientService metalArchivesClientService)
        {
            _metalArchivesClientService = metalArchivesClientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetArtists(string query)
        {
            var artists = await _metalArchivesClientService.FetchArtists(query);
            return Ok(artists);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetArtistById(string id)
        {
            var artist = await _metalArchivesClientService.FetchArtistById(id);
            return Ok(artist);
        }
    }
}
