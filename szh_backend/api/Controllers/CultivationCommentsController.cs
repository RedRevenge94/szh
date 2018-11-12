using Microsoft.AspNetCore.Mvc;
using szh.cultivation;

namespace api.Controllers {
    [Route("api/[controller]")]
    public class CultivationCommentsController : Controller {

        [HttpGet("{id}", Name = "GetCultivationComment")]
        public IActionResult Get(int id) {
            return new ObjectResult(CultivationComment.GetCultivationComment(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CultivationComment cultivationComment) {

            if (cultivationComment.text == null || cultivationComment.cultivation == null) {
                return BadRequest();
            } else {
                CultivationComment newCultivationComment = CultivationComment.AddCultivationComents(cultivationComment.text, cultivationComment.cultivation);
                if (newCultivationComment.text.Equals(cultivationComment.text)) {
                    return CreatedAtRoute("GetCultivationComment", new { newCultivationComment.id }, newCultivationComment);
                }
            }
            return BadRequest();
        }

    }
}
