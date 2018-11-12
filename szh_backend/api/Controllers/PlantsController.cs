using Microsoft.AspNetCore.Mvc;
using szh.cultivation;

namespace api.Controllers {
    [Route("api/[controller]")]
    public class PlantsController : Controller {

        // GET api/tunnels
        [HttpGet]
        public IActionResult Get() {
            return new ObjectResult(Plant.GetPlants());
        }

    }
}

