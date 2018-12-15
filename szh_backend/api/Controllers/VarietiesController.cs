using Microsoft.AspNetCore.Mvc;
using szh.cultivation;
using szh.dao;

namespace api.Controllers {
    [Route("api/[controller]")]
    public class VarietiesController : Controller {

        // GET api/Varieties
        [HttpGet("{varietyId}", Name = "GetVariety")]
        public IActionResult Get(int varietyId) {
            return new ObjectResult(Variety.GetVariety(varietyId));
        }

        // GET api/Varieties
        [HttpGet("plant/{plantSpeciesId}", Name = "GetVarietiesForPlant")]
        public IActionResult GetVarieties(int plantSpeciesId) {
            return new ObjectResult(Variety.GetVarietyForPlantSpecies(plantSpeciesId));
        }

        // GET api/variety/add
        [HttpPost("add", Name = "CreateVariety")]
        public IActionResult AddVariety([FromBody] VarietyInfo varietyInfo) {

            if (varietyInfo.name == null || varietyInfo.plantSpeciesId == 0) {
                return BadRequest();
            } else {
                Variety newVariety = VarietyInfo.CreateVariety(varietyInfo.plantSpeciesId, varietyInfo.name);
                if (newVariety.name.Equals(varietyInfo.name)) {
                    return Ok();
                }
            }
            return BadRequest();
        }

    }
}
