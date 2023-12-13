using MetalCollector.WebApi.Dtos;
using MetalCollector.WebApi.Models;

namespace MetalCollector.WebApi.Services.Interfaces
{
    public interface IItemService
    {
        Task<Item> ItemAdd(ItemDto item);
        Task<IEnumerable<ItemDto>> GetItems();
        Task<ItemDto> DeleteItems(string itemId);
    }
}
