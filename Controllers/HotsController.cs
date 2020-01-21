using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PtViewer.Models;
using PtViewer.Services;

namespace PtViewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotsController : ControllerBase
    {
        private readonly ItemService _ItemService;

        public HotsController(ItemService ItemService)
        {
            _ItemService = ItemService;
        }

        [HttpGet]
        public ActionResult<List<Hot>> Get([FromQuery]string search,
         [FromQuery]string source,
         [FromQuery]int page = 1) =>
            _ItemService.GetHot(search, page);
    }
}
