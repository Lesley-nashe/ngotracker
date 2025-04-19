using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ngotracker.Models.NgoModels;
using ngotracker.Services.NgoServices;

namespace ngotracker.Controllers.NgoControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NgoController(INgoService ngoService) : ControllerBase
    {
        private readonly INgoService _ngoService = ngoService;

        [HttpPost("ngo")]
        public async Task<IActionResult> CreateNgo([FromBody] NgoModel ngo)
        {
            var creation = await _ngoService.CreateNgo(ngo);
            if (creation)
            {
                return Ok("Ngo Created");
            }
            return BadRequest("Failed to create Ngo");
        }

        [HttpGet("ngo")]
        public async Task<IActionResult> GetNgo(Guid Id)
        {
            var ngo = _ngoService.GetNgo(Id);
            if (ngo is not null)
            {
                return Ok(ngo);
            }

            else return BadRequest("ngo not found");

        }

        [HttpDelete("ngo")]
        public async Task<IActionResult> DeleteNgo([FromBody] Guid Id)
        {
            var ngoDeleted = _ngoService.DeleteNgo(Id).Result;
            if (ngoDeleted)
            {
                return Ok("ngo Deleted");
            }
            return BadRequest("Deletion failed");
        }

        [HttpPut("ngo")]
        public async Task<IActionResult> UpdateNgo(Guid id, [FromBody] NgoModel ngo)
        {
            var updatedNgo = _ngoService.UpdateNgo(id, ngo);
            if (updatedNgo is not null)
            {
                return Ok(updatedNgo);
            }
            return BadRequest("Ngo Update failed");
        }

        [HttpPatch("ngo")]
        public async Task<IActionResult> EditNgo([FromBody] NgoModel ngo)
        {
            return Ok();
        }
    }
}
