using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using szh.cultivation;
using szh.dao;

namespace api.Controllers {
    [Route("api/[controller]")]
    public class TunnelsInfoController : Controller {

        // GET api/tunnels
        [HttpGet]
        public IActionResult Get() {
            return new ObjectResult(TunnelInfo.GetTunnelsInfo());
        }

    }
}

