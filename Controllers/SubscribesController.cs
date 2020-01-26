using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PtViewer.Models;
using PtViewer.Services;

namespace PtViewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribesController : ControllerBase
    {
        private readonly ItemService _itemService;

        public SubscribesController(ItemService ItemService)
        {
            _itemService = ItemService;
        }

        [HttpGet]
        public ActionResult<Subscribe[]> Get(int count = 10) =>
            _itemService.GetSubscribes(count);

        [HttpPost("{sub}")]
        public IActionResult Subscribe(string sub)
        {
            return new JsonResult(_itemService.Subscribe(sub));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _itemService.RemoveSubscribe(id);

            return NoContent();
        }
    }
}
