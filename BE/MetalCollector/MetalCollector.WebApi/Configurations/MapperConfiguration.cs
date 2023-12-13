using AutoMapper;
using MetalCollector.WebApi.Dtos;
using MetalCollector.WebApi.Models;

namespace MetalCollector.WebApi.Configurations
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<Artist, ArtistMADto>()
                .ForMember(dest => dest.Discography, opt => opt.MapFrom(src => src.Discographies))
                .ReverseMap();
            CreateMap<Lineup, LineupDto>().ReverseMap();
            CreateMap<Social, SocialDto>().ReverseMap();
            CreateMap<Discography, DiscographyDto>().ReverseMap();
        }
    }
}
