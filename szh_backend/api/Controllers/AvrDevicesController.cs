using Microsoft.AspNetCore.Mvc;
using szh.cultivation;
using szh.dao;

namespace api.Controllers {
    [Route("api/[controller]")]
    public class AvrDevicesInfoController : Controller {

        // GET api/tunnels
        [HttpGet]
        public IActionResult Get() {
            return new ObjectResult(AvrDeviceInfo.GetAvrDeviceInfo());
        }

        [HttpGet("{id}", Name = "GetAvrDevice")]
        public IActionResult Get(int id) {
            return new ObjectResult(AvrDevice.GetAvrDevice(id));
        }

        [HttpPost(Name = "CreateAvrDevice")]
        public IActionResult Create([FromBody] AvrDevice avrDevice) {

            if (avrDevice.ip == null || avrDevice.tunnel == null) {
                return BadRequest();
            } else {
                AvrDevice avrChanged = AvrDevice.CreateAvrDevice(avrDevice.ip, avrDevice.tunnel.id);
                if (avrChanged.ip.Equals(avrDevice.ip)) {
                    return CreatedAtRoute("GetTunnel", new { avrChanged.id }, avrChanged);
                }
            }
            return BadRequest();
        }
    }
}

