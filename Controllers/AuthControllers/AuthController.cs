using jobtrackerapi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ngotracker.Models;
using ngotracker.Models.AuthModels;

namespace ngotracker.Controllers.AuthControllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (await _authService.RegisterUser(model))
            {
                return Ok("Successfully Registered User");
            }

            return BadRequest("Failed To Register User");
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("User Input contains an error");
            }

            var loginResult = await _authService.LoginUser(model);

            if (loginResult.IsLogged)
            {
                return Ok(loginResult);
            }

            return Unauthorized("Login Failed");
        }

        [HttpPost("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetUser([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("User Input contains an error");
            }

            var user = await _authService.GetUser(model);

            if (user is not null)
            {
                return Ok(user);
            }

            return BadRequest("User Does not exist");

        }

        [HttpPost("refreshtoken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
