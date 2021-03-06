using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PtViewer.Models;
using PtViewer.Services;

namespace PtViewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly ItemService _itemService;

        public FavoritesController(ItemService ItemService)
        {
            _itemService = ItemService;
        }

        [HttpGet]
        public ActionResult<List<Item>> Get([FromQuery]string search,
         [FromQuery]string source,
         [FromQuery]int page = 1) =>
            _itemService.GetFavorateItem(search, page, source);

        [HttpPost("{id}")]
        public IActionResult Faverate(string id)
        {
            var item = _itemService.GetItemById(id);

            if (item == null)
            {
                return NotFound();
            }

            if (null == item.Favorators)
            {
                item.Favorators = new int[] { 1 };
                _itemService.Update(id, item);
            }
            else
            {
                item.Favorators = null;
                _itemService.Update(id, item);
            }

            return NoContent();
        }

        // [HttpDelete("{id}")]
        // public IActionResult Delete(string id)
        // {
        //     var item = _itemService.GetItemById(id);

        //     if (item == null)
        //     {
        //         return NotFound();
        //     }

        //     if (null != item.Favorators && item.Favorators.Contains(1))
        //     {
        //         item.Favorators = null;
        //         _itemService.Update(id, item);
        //     }

        //     return NoContent();
        // }

        // [HttpGet("{id:length(24)}", Name = "GetItem")]
        // public ActionResult<Item> GetById(string id)
        // {
        //     var Item = _ItemService.GetItemById(id);

        //     if (Item == null)
        //     {
        //         return NotFound();
        //     }

        //     return Item;
        // }

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

    }
}
