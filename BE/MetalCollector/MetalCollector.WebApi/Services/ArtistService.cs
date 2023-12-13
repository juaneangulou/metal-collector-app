using MetalCollector.WebApi.Dtos;
using MetalCollector.WebApi.Models;
using MetalCollector.WebApi.Repositories.Interfaces;
using MetalCollector.WebApi.Services.Interfaces;

namespace MetalCollector.WebApi.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IBaseRepository<Artist> _artistRepository;
        private readonly IBaseRepository<Lineup> _lineupRepository;
        private readonly IBaseRepository<Social> _socialRepository;


        public ArtistService(IBaseRepository<Artist> artistRepository, IBaseRepository<Lineup> lineupRepository, IBaseRepository<Social> socialRepository) 
        { 
            _artistRepository = artistRepository;
            _lineupRepository = lineupRepository;
            _socialRepository = socialRepository;
            
        }
        public Task ArtistAdd(ArtistMADto artist)
        {
            throw new NotImplementedException();
        }

        public Task<ArtistMADto> GetArtistById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
