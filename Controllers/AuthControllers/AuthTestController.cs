using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ngotracker.Controllers.AuthControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthTestController : ControllerBase
    {
        [HttpGet("Test")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Test()
        {
            return Ok("Not authorisation checking but worked");
        }
        
        [HttpGet("AuthorizedTest")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult AuthorizedTest()
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            string jwtSecurityToken = authorizationHeader.Replace("Bearer ","");

            var jwt = new JwtSecurityToken(jwtSecurityToken);

            var response = $"Authenticated! {Environment.NewLine}";

            response += $"{Environment.NewLine} Exp Time: {jwt.ValidTo.ToLongTimeString()}, Time: {DateTime.Now.ToLongDateString()}";

            return Ok(response);
        }
    }
}
