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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNgo([FromBody] NgoModel ngo)
        {
            if (ngo is null)
                return BadRequest("Invalid NGO data.");

            var created = await _ngoService.CreateNgo(ngo);
            if (!created)
                return BadRequest("Failed to create NGO.");

            return CreatedAtAction(nameof(GetNgoById), new { id = ngo.Id }, ngo);
        }

        [HttpGet("ngo/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNgoById(Guid id)
        {
            var ngo = await _ngoService.GetNgo(id);

            if (ngo is null)
                return NotFound($"NGO with ID {id} not found.");

            return Ok(ngo);
        }

        [HttpGet("ngos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNgos()
        {
            var ngos = await _ngoService.GetNgos();
            if (ngos is null || !ngos.Any())
                return NotFound("No NGOs found.");
            if (!ngos.Any())
                return Ok(new List<NgoModel>());
            return Ok(ngos);
        }

        [HttpDelete("ngo/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNgo(Guid id)
        {
            var ngoDeleted = await _ngoService.DeleteNgo(id);
            if (!ngoDeleted)
                return NotFound($"NGO with ID {id} not found or could not be deleted.");

            return Ok("NGO deleted successfully.");
        }

        [HttpPut("ngo/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateNgo(Guid id, [FromBody] NgoModel ngo)
        {
            if (ngo is null || id != ngo.Id)
                return BadRequest("Invalid NGO data or mismatched ID.");
            var updatedNgo = await _ngoService.UpdateNgo(id, ngo);
            if (updatedNgo is null) return NotFound($"NGO with ID {id} not found or could not be updated.");
            return Ok(updatedNgo);
        }

        [HttpPatch("ngo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditNgo([FromBody] NgoModel ngo)
        {
            return Ok();
        }
    }
}
