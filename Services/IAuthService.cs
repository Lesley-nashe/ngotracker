using System;
using jobtrackerapi.Models;

namespace jobtrackerapi.Services;

public interface IAuthService
{
    Task<bool> RegisterUser(RegisterModel model);
    Task<bool> LoginUser(LoginModel model);

}
