using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ngotracker.Models.ApplicationModels;
using ngotracker.Services.ApplicationServices;

namespace ngotracker.Controllers.ApplicationControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        [HttpPost("application")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateApplication([FromBody] ApplicationModel application)
        {
            if (application is null) return BadRequest("Invalid Application data.");
            var created = await _applicationService.CreateApplication(application);
            if (!created) return BadRequest("Failed to create Application.");
            return CreatedAtAction(nameof(GetApplicationById), new { id = application.Id }, application);
        }

        [HttpGet("applications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetApplications()
        {
            var applications = await _applicationService.GetApplications();
            if (applications is null || !applications.Any())
                return NotFound("No Applications found.");
            return Ok(applications);
        }

        [HttpGet("application/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetApplicationById(Guid Id)
        {
            var application = await _applicationService.GetApplication(Id);
            if (application is null) return NotFound($"Application with ID {Id} not found.");
            return Ok(application);
        }

        [HttpDelete("application/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteApplication(Guid Id)
        {
            var applicationDeleted = await _applicationService.DeleteApplication(Id);
            if (!applicationDeleted) return NotFound($"Application with ID {Id} not found or could not be deleted.");
            return Ok("Application deleted successfully.");
        }

        [HttpPut("application/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateApplication(Guid id, [FromBody] ApplicationModel application)
        {
            if (application is null || id != application.Id)
                return BadRequest("Invalid Application data or mismatched ID.");
            var updatedApplication = await _applicationService.UpdateApplication(id, application);
            if (updatedApplication is null) return NotFound($"Application with ID {id} not found or could not be updated.");
            return Ok(updatedApplication);
        }

        [HttpPatch("application")]
        public async Task<IActionResult> editGrant([FromBody] ApplicationModel application)
        {
            return Ok();
        }
    }
}
