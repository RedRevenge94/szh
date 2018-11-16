using Microsoft.AspNetCore.Mvc;
using szh.cultivation;
using szh.cultivation.plants;

namespace api.Controllers {
    [Route("api/[controller]")]
    public class PlantsController : Controller {

        // GET api/tunnels
        [HttpGet]
        public IActionResult Get() {
            return new ObjectResult(PlantSpeciesInfo.GetPlantSpeciesInfo());
        }

        // GET api/tunnels
        [HttpGet("{id}", Name = "GetPlant")]
        public IActionResult GetPlant(int id) {
            return new ObjectResult(PlantSpeciesInfo.GetPlantSpeciesInfo(id));
        }

    }
}

