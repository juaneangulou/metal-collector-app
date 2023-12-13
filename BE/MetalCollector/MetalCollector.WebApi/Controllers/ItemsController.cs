using MetalCollector.WebApi.Dtos;
using MetalCollector.WebApi.Models;
using MetalCollector.WebApi.Repositories.Interfaces;
using MetalCollector.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetalCollector.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMetalArchivesClientService _metalArchivesClientService;
        private readonly IItemService _itemService;
        public ItemsController(IMetalArchivesClientService metalArchivesClientService, IItemService itemService)
        {
            _metalArchivesClientService = metalArchivesClientService;
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items =  await _itemService.GetItems();
            return Ok(items);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetItemById(string id)
        {
            var item = "";// await _metalArchivesClientService.FetchItemById(id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(ItemDto item)
        {
          var response=  await _itemService.ItemAdd(item);// await _metalArchivesClientService.(item);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            var response = await _itemService.DeleteItems(id);// await _metalArchivesClientService.(item);
            return Ok(response);
        }

        //[HttpGet]
        //[Route("{id}")]
        //public async Task<IActionResult> GetItemById(string id)
        //{
        //    var item = await _metalArchivesClientService.FetchItemById(id);
        //    return Ok(item);
        //}     
    }
}
