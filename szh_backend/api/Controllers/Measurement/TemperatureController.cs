using Microsoft.AspNetCore.Mvc;
using System;
using szh.dao;
using szh.measurement;

namespace api.Controllers {
    [Route("api/[controller]")]
    public class TemperatureController : Controller {

        [HttpGet("tunnel/{id}", Name = "GetTemperatureForTunnelLastThreeDays")]
        public IActionResult GetTemperatureForTunnelTimeRange(int id, [FromQuery(Name = "startDate")] DateTime startDate,
            [FromQuery(Name = "endDate")] DateTime endDate) {
            if (endDate.Date == DateTime.Now.Date) {
                endDate = endDate.Add(DateTime.Now - endDate);
            }
            return new ObjectResult(Measurement.GetTemperatureInTunnel(id, startDate, endDate));
        }

    }
}
