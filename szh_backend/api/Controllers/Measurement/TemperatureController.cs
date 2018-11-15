using Microsoft.AspNetCore.Mvc;
using System;
using szh.measurement;

namespace api.Controllers {
    [Route("api/[controller]")]
    public class TemperatureController : Controller {

        [HttpGet("tunnel/{id}", Name = "GetTemperatureForTunnelLastThreeDays")]
        public IActionResult GetTemperatureForTunnelLastThreeDays(int id) {
            return new ObjectResult(Measurement.GetTemperatureInTunnel(id, DateTime.Now.AddDays(-3), DateTime.Now));
        }

    }
}
