using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PtViewer.Models;
using PtViewer.Services;

namespace PtViewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemService _ItemService;

        public ItemsController(ItemService ItemService)
        {
            _ItemService = ItemService;
        }

        [HttpGet]
        public ActionResult<List<Item>> Get([FromQuery]string search,
         [FromQuery]string source,
         [FromQuery]int page = 1) =>
            _ItemService.GetItem(search, page, source);

        [HttpGet("{id:length(24)}", Name = "GetItem")]
        public ActionResult<Item> GetById(string id)
        {
            var Item = _ItemService.GetItemById(id);

            if (Item == null)
            {
                return NotFound();
            }

            return Item;
        }

        // [HttpPost]
        // public ActionResult<Item> Create(Item Item)
        // {
        //     _ItemService.Create(Item);

        //     return CreatedAtRoute("GetItem", new { id = Item.Id.ToString() }, Item);
        // }

        // [HttpPut("{id:length(24)}")]
        // public IActionResult Update(string id, Item ItemIn)
        // {
        //     var Item = _ItemService.GetItemById(id);

        //     if (Item == null)
        //     {
        //         return NotFound();
        //     }

        //     _ItemService.Update(id, ItemIn);

        //     return NoContent();
        // }

        // [HttpDelete("{id:length(24)}")]
        // public IActionResult Delete(string id)
        // {
        //     var Item = _ItemService.GetItemById(id);

        //     if (Item == null)
        //     {
        //         return NotFound();
        //     }

        //     _ItemService.Remove(Item.Id);

        //     return NoContent();
        // }
    }
}
