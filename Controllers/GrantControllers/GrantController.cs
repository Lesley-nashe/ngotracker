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
        public async Task<IActionResult> CreateGrant([FromBody] GrantModel grant)
        {
            var creation = await _grantService.CreateGrant(grant);
            if (creation)
            {
                return Ok("Grant Created");
            }
            return BadRequest("Failed to create Grant");
        }

        [HttpGet("grant")]
        public async Task<IActionResult> GetGrant(Guid Id)
        {
            var grant = _grantService.GetGrant(Id);
            if (grant is not null)
            {
                return Ok(grant);
            }

            else return BadRequest("Grant not found");
        }

        [HttpDelete("grant")]
        public async Task<IActionResult> DeleteGrant([FromBody] Guid Id)
        {
            var grantDeleted = _grantService.DeleteGrant(Id).Result;
            if (grantDeleted)
            {
                return Ok("Grant Deleted");
            }
            return BadRequest("Deletion failed");
        }

        [HttpPut("grant")]
        public async Task<IActionResult> updateGrant(Guid id,[FromBody] GrantModel grant)
        {
            var updatedGrant = _grantService.UpdateGrant(id, grant);
            if (updatedGrant is not null)
            {
                return Ok(updatedGrant);
            }
            return BadRequest("Grant Update failed");
        }

        [HttpPatch("grant")]
        public async Task<IActionResult> editGrant([FromBody] GrantModel grant)
        {
            return Ok();
        }
    }
}
