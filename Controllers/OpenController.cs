using Microsoft.AspNetCore.Mvc;
using PtViewer.Models;
using PtViewer.Services;

namespace PtViewer.Controllers
{
    [Route("[controller]")]
    [Controller]
    public class OpenController : Controller
    {
        private readonly ItemService _ItemService;
        private readonly UrlMapping _urlMapping;

        public OpenController(ItemService ItemService, UrlMapping urlMapping)
        {
            _ItemService = ItemService;
            _urlMapping = urlMapping;
        }
        [HttpGet]
        public IActionResult Get([FromQuery]string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var item = _ItemService.GetItemById(id);
            if (null == item || !_urlMapping.ContainsKey(item.Source))
            {
                return NotFound();
            }

            var url = string.Concat(_urlMapping[item.Source], item.Link);

            return Redirect(url);
        }
    }
}
