using Microsoft.AspNetCore.Mvc;
using szh.measurement;

namespace api.Controllers {
    [Route("api/[controller]")]
    public class MeasurementController : Controller {

        [HttpGet("{id}", Name = "GetMeasurement")]
        public IActionResult Get(int id) {
            return new ObjectResult(Measurement.GetMeasurement(id));
        }

        [HttpPost]
        public IActionResult PostMeasurement([FromBody] Measurement measurement) {

            if (measurement.measurement_type == null || measurement.avr_device == null || measurement.value == null) {
                return BadRequest();
            } else {
                Measurement newMeasurement = Measurement.AddMeasurement(measurement);
                if (newMeasurement.value.Equals(measurement.value)) {
                    return CreatedAtRoute("GetMeasurement", new { newMeasurement.id }, newMeasurement);
                }
            }
            return BadRequest();
        }

    }
}
