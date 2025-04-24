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
            var creation = await _applicationService.CreateApplication(application);
            if (creation)
            {
                return Ok("application Created");
            }
            return BadRequest("Failed to create Application");
        }

        [HttpGet("applications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetApplications()
        {
            var applications = _applicationService.GetApplications();
            if (applications is not null)
            {
                return Ok(applications);
            }

            else return BadRequest("Applications not found");
        }

        [HttpGet("application")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetApplication(Guid Id)
        {
            var application = _applicationService.GetApplication(Id);
            if (application is not null)
            {
                return Ok(application);
            }

            else return BadRequest("Application not found");
        }

        [HttpDelete("application")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteApplication([FromBody] Guid Id)
        {
            var applicationDeleted = _applicationService.DeleteApplication(Id).Result;
            if (applicationDeleted)
            {
                return Ok("Application Deleted");
            }
            return BadRequest("Deletion failed");
        }

        [HttpPut("application")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateApplication(Guid id, [FromBody] ApplicationModel application)
        {
            var updatedApplication = _applicationService.UpdateApplication(id, application);
            if (updatedApplication is not null)
            {
                return Ok(updatedApplication);
            }
            return BadRequest("Application Update failed");
        }

        [HttpPatch("application")]
        public async Task<IActionResult> editGrant([FromBody] ApplicationModel application)
        {
            return Ok();
        }
    }
}
