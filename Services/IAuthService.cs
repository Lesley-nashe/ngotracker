using System;
using jobtrackerapi.Models;
using Microsoft.AspNetCore.Identity;

namespace jobtrackerapi.Services;

public interface IAuthService
{
    Task<bool> RegisterUser(RegisterModel model);
    Task<bool> LoginUser(LoginModel model);

    string GenerateJwtToken(LoginModel loginuser);
    Task<IdentityUser> GetUser(LoginModel model);

}
