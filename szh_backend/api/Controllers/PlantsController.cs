using Microsoft.AspNetCore.Mvc;
using szh.cultivation;
using szh.cultivation.plants;
using szh.dao;

namespace api.Controllers {
    [Route("api/[controller]")]
    public class PlantsController : Controller {

        // GET api/plants
        [HttpGet]
        public IActionResult Get() {
            return new ObjectResult(PlantSpeciesInfo.GetPlantSpeciesInfo());
        }

        // GET api/plants
        [HttpGet("{id}", Name = "GetPlant")]
        public IActionResult GetPlant(int id) {
            return new ObjectResult(PlantSpeciesInfo.GetPlantSpeciesInfo(id));
        }

        // GET api/plant/add/id
        [HttpPost("add", Name = "CreatePlantSpecies")]
        public IActionResult AddPlant([FromBody] PlantSpecies plantSpeciesRequest) {

            if (plantSpeciesRequest.name == null) {
                return BadRequest();
            } else {
                PlantSpecies plantSpecies = PlantSpecies.AddPlantSpecies(plantSpeciesRequest.name);
                if (plantSpecies.name.Equals(plantSpeciesRequest.name)) {
                    return CreatedAtRoute("GetPlant", new { plantSpecies.id }, plantSpecies);
                }
            }
            return BadRequest();
        }

    }
}

