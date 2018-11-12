using Microsoft.AspNetCore.Mvc;
using szh.cultivation;

namespace api.Controllers {
    [Route("api/[controller]")]
    public class TunnelsController : Controller {

        // GET api/tunnels
        [HttpGet]
        public IActionResult Get() {
            return new ObjectResult(Tunnel.GetTunnels());
        }

        [HttpGet("{id}", Name = "GetTunnel")]
        public IActionResult Get(int id) {
            return new ObjectResult(Tunnel.GetTunnel(id));
        }

        [HttpPost("{name}", Name = "CreateTunnel")]
        public IActionResult Create(string name) {

            if (name == null) {
                return BadRequest();
            } else {
                Tunnel tunnelChanged = Tunnel.CreateTunnel(name);
                if (tunnelChanged.name.Equals(name)) {
                    return CreatedAtRoute("GetTunnel", new { tunnelChanged.id }, tunnelChanged);
                }
            }
            return BadRequest();
        }

        [HttpPut("{name}", Name = "UpdateTunnel")]
        public IActionResult Update(string name, [FromBody] Tunnel tunnel) {
            if (tunnel == null || tunnel.name == null) {
                return BadRequest();
            } else {
                Tunnel tunnelChanged = Tunnel.UpdateTunnel(tunnel, name);
                if (tunnel.name.Equals(name)) {
                    return new NoContentResult();
                }
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            Tunnel.DeleteTunnel(id);
            return new NoContentResult();
        }
    }
}

