using System;
using ngotracker.Models.AuthModels;

namespace jobtrackerapi.Services;

public interface IAuthService
{
    Task<bool> RegisterUser(RegisterModel model);
    Task<LoginResponse> LoginUser(LoginModel model);
    string GenerateJwtToken(string userName);
    string GenerateRefreshToken();
    Task<LoginResponse> RefreshToken(RefreshTokenModel model);
    Task<UserModel?> GetUser(LoginModel model);
}
