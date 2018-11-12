using Microsoft.AspNetCore.Mvc;
using szh.cultivation;
using szh.dao;

namespace api.Controllers {
    [Route("api/[controller]")]
    public class CultivationsInfoController : Controller {

        // GET api/cultivation
        [HttpGet]
        public IActionResult Get() {
            return new ObjectResult(CultivationInfo.GetCultivationInfo());
        }

        // GET api/cultivation/basic
        [HttpGet("basic")]
        public IActionResult GetBasic() {
            return new ObjectResult(CultivationInfoBasic.GetCultivationInfo());
        }

        [HttpGet("{id}", Name = "GetCultivation")]
        public IActionResult Get(int id) {
            return new ObjectResult(CultivationInfo.GetCultivationInfo(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] Cultivation cultivation) {

            if (cultivation.name == null || cultivation.plant == null || cultivation.start_date == null) {
                return BadRequest();
            } else {
                Cultivation newCultivation = Cultivation.CreateCultivation(cultivation);
                if (newCultivation.name.Equals(cultivation.name)) {
                    return CreatedAtRoute("GetCultivation", new { newCultivation.id }, newCultivation);
                }
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Cultivation cultivation) {

            if (cultivation.name == null || cultivation.start_date == null) {
                return BadRequest();
            } else {
                Cultivation newCultivation = Cultivation.UpdateCultivation(cultivation);
                if (newCultivation.name.Equals(cultivation.name)) {
                    return CreatedAtRoute("GetCultivation", new { newCultivation.id }, newCultivation);
                }
            }
            return BadRequest();
        }

    }
}

