using Microsoft.AspNetCore.Mvc;
using System;
using szh.measurement;

namespace api.Controllers {
    [Route("api/[controller]")]
    public class TemperatureController : Controller {

        [HttpGet("tunnel/{id}", Name = "GetTemperatureForTunnelLastWeek")]
        public IActionResult GetTemperatureForTunnelLastWeek(int id) {
            return new ObjectResult(Measurement.GetTemperatureInTunnel(id, DateTime.Now.AddDays(-7), DateTime.Now));
        }

    }
}
