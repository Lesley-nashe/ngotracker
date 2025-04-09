using jobtrackerapi.Models;
using jobtrackerapi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace jobtrackerapi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (await _authService.RegisterUser(model))
            {
                return Ok("Successfully Registered User");
            }

            return BadRequest("Failed To Register User");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (await _authService.LoginUser(model))
            {
                var token = _authService.GenerateJwtToken(model);
                return Ok(token);
            }

            return BadRequest("Login Failed");
        }

        [HttpPost("user")]

        public async Task<IActionResult> GetUser([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _authService.GetUser(model);

            if (user is not null)
            {
                return Ok(user);
            }

            return BadRequest("User Does not exist");

        }
    }

}
