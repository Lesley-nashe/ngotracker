using jobtrackerapi.Models;
using jobtrackerapi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ngotracker.Models;

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

            var loginResult = await _authService.LoginUser(model);

            if (loginResult.IsLogged)
            {
                return Ok(loginResult);
            }

            return Unauthorized("Login Failed");
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

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel model)
        {
            var loginResult = await _authService.RefreshToken(model);
            if (loginResult.IsLogged)
            {
                return Ok(loginResult);
            }
            return Unauthorized();
        }
    }

}
