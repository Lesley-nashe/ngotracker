using System;
using jobtrackerapi.Models;
using Microsoft.AspNetCore.Identity;
using ngotracker.Models;

namespace jobtrackerapi.Services;

public interface IAuthService
{
    Task<bool> RegisterUser(RegisterModel model);
    Task<Loginresponse> LoginUser(LoginModel model);

    string GenerateJwtToken(string userName);
    string GenerateRefreshToken();

    Task<Loginresponse> RefreshToken(RefreshTokenModel model);
    Task<IdentityUser> GetUser(LoginModel model);

}
