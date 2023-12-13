using AutoMapper;
using MetalCollector.WebApi.Dtos;
using MetalCollector.WebApi.Models;
using MetalCollector.WebApi.Repositories.Interfaces;
using MetalCollector.WebApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MetalCollector.WebApi.Services
{
    public class ItemService : IItemService
    {
        private readonly IBaseRepository<Band> _bandRepository;
        private readonly IBaseRepository<Member> _memberRepository;
        private readonly IBaseRepository<Release> _releaseRepository;
        private readonly IBaseRepository<Item> _itemRepository;
        private readonly IBaseRepository<Artist> _artistRepository;
        private readonly IBaseRepository<Discography> _discographyRepository;
        private readonly IBaseRepository<Lineup> _lineupRepository;
        private readonly IBaseRepository<Social> _socialRepository;
        private readonly IMetalArchivesClientService _metalArchivesClientService;
        private readonly IMapper _mapper;
        private readonly MetalCollectorDbContext _context;
        public ItemService(
            IBaseRepository<Band> bandRepository,
            IBaseRepository<Member> memberRepository,
            IBaseRepository<Release> releaseRepository,
            IBaseRepository<Item> itemRepository,
            IBaseRepository<Artist> artistRepository,
            IBaseRepository<Discography> discographyRepository,
            IBaseRepository<Lineup> lineupRepository,
            IBaseRepository<Social> socialRepository, IMapper mapper, MetalCollectorDbContext context, IMetalArchivesClientService metalArchivesClientService)
        {
            _bandRepository = bandRepository;
            _memberRepository = memberRepository;
            _releaseRepository = releaseRepository;
            _itemRepository = itemRepository;
            _artistRepository = artistRepository;
            _discographyRepository = discographyRepository;
            _lineupRepository = lineupRepository;
            _socialRepository = socialRepository;
            _mapper = mapper;
            _context = context;
            _metalArchivesClientService = metalArchivesClientService;
        }


        public async Task<Item> ItemAdd(ItemDto item)
        {
            try
            {
                if (!string.IsNullOrEmpty(item.ArtistId))
                {
                    var artist =  await _artistRepository.FindBy(x => x.Id == item.ArtistId).FirstOrDefaultAsync();
                    if (artist == null)
                    {
                        var artistFromMA = await _metalArchivesClientService.FetchArtistById(item.ArtistId);
                        if (artistFromMA != null)
                        {
                           await AddArtist(artistFromMA);

                        }
                    }                   
                }

                var itemEntity = _mapper.Map<Item>(item);
                itemEntity.ItemId = Guid.NewGuid().ToString();
                await _itemRepository.Add(itemEntity);

                return itemEntity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ItemDto>> GetItems()
        {
            var items = await _itemRepository.GetAll().AsNoTracking().ToListAsync();
            if (items == null)
                return null;

            var itemsDto = _mapper.Map<IEnumerable<ItemDto>>(items);
            foreach (var item in itemsDto)
            {
                if (!string.IsNullOrEmpty(item.ArtistId))
                {
                    var artist = await _artistRepository.FindBy(x => x.Id == item.ArtistId).FirstOrDefaultAsync();
                    if (artist != null)
                    {
                        item.Artists = _mapper.Map<ArtistMADto>(artist);
                    }
                }
            }
            return itemsDto;
        }
        private async Task AddArtist(ArtistMADto artistFromMA)
        {
            if (artistFromMA == null)
                throw new NotImplementedException();

            var response = _mapper.Map<Artist>(artistFromMA);

            if (response.Discographies != null && response.Discographies.Any())
            {
                response.Discographies = response.Discographies.Select((x) =>
                {
                    x.Id = Guid.NewGuid().ToString();
                    return x;
                }).ToList();
            }

            if (response.Lineup != null && response.Lineup.Any())
            {
                response.Lineup = response.Lineup.Select((x) =>
                {
                    x.ArtistId = response.Id;
                    return x;
                }).ToList();
            }

            if (response.Social != null && response.Social.Any())
            {
                response.Social = response.Social.Select((x) =>
                {
                    x.Id = Guid.NewGuid().ToString();
                    x.ArtistId = response.Id;
                    return x;
                }).ToList();
            }

            await _artistRepository.Add(response);

        }

        public async Task<ItemDto> DeleteItems(string itemId)
        {
            var items = await _itemRepository.FindBy(x=>x.ItemId==itemId).FirstOrDefaultAsync();
            if (items == null)
                return null;

            await _itemRepository.Delete(items);
            var itemsDto = _mapper.Map<ItemDto>(items);
            return itemsDto;
        }
    }
}
