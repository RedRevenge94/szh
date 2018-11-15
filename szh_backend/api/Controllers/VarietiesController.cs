using Microsoft.AspNetCore.Mvc;
using szh.cultivation;

namespace api.Controllers {
    [Route("api/[controller]")]
    public class VarietiesController : Controller {

        // GET api/Varieties
        [HttpGet("{plantId}")]
        public IActionResult Get(int plantId) {
            return null;
        }

    }
}
