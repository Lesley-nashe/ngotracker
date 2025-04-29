using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ngotracker.Models.GrantModels;
using ngotracker.Services.GrantServices;

namespace ngotracker.Controllers.GrantControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrantController : ControllerBase
    {
        private readonly IGrantService _grantService;
        public GrantController(IGrantService grantService)
        {
            _grantService = grantService;

        }
        [HttpPost("grant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGrant([FromBody] GrantModel grant)
        {
            if (grant is null) return BadRequest("Invalid Grant data.");
            var creation = await _grantService.CreateGrant(grant);
            if (!creation)
                return BadRequest("Failed to create NGO.");
            return CreatedAtAction(nameof(GetGrantbyId), new { id = grant.Id }, grant);
        }

        [HttpGet("grant/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGrantbyId(Guid Id)
        {
            var grant = await _grantService.GetGrant(Id);
            if (grant is null)
                return NotFound($"Grant with ID {Id} not found.");

            return Ok(grant);
        }

        [HttpGet("grants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetGrants()
        {
            var grants = await _grantService.GetGrants();
            if (grants is null || !grants.Any())
                return NotFound("No Grants found.");

            return Ok(grants);
        }

        [HttpDelete("grant/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGrant(Guid Id)
        {
            var grantDeleted = await _grantService.DeleteGrant(Id);
            if (!grantDeleted)
                return NotFound($"Grant with ID {Id} not found or could not be deleted.");
            return Ok("Grant deleted successfully.");
        }

        [HttpPut("grant")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateGrant(Guid id, [FromBody] GrantModel grant)
        {
            if (grant is null || id != grant.Id)
                return BadRequest("Invalid Grant data or mismatched ID.");
            var updatedGrant = await _grantService.UpdateGrant(id, grant);
            if (updatedGrant is null)
            {
                return NotFound($"Grant with ID {id} not found or could not be updated.");
            }
            return Ok(updatedGrant);
        }

        [HttpPatch("grant")]
        public async Task<IActionResult> editGrant([FromBody] GrantModel grant)
        {
            return Ok();
        }
    }
}
