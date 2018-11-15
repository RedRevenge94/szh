using Microsoft.AspNetCore.Mvc;
using szh.cultivation;

namespace api.Controllers {
    [Route("api/[controller]")]
    public class VarietiesController : Controller {

        // GET api/Varieties
        [HttpGet("{plantSpeciesId}")]
        public IActionResult Get(int plantSpeciesId) {
            return new ObjectResult(Variety.GetVarietyForPlantSpecies(plantSpeciesId));
        }

    }
}
