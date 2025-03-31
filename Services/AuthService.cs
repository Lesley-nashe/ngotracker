using System;
using jobtrackerapi.Models;
using Microsoft.AspNetCore.Identity;

namespace jobtrackerapi.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager = null!;
    private readonly SignInManager<IdentityUser> _signInManager = null!;
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task<bool> LoginUser(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is null)
        {
            return false;
        }
        return await _userManager.CheckPasswordAsync(user, model.Password);

    }

    public async Task<bool> RegisterUser(RegisterModel model)
    {
        var IdentityUser = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email,
        };

        var result = await _userManager.CreateAsync(IdentityUser, model.Password);

        Console.WriteLine(result);

        return result.Succeeded;
    }
}
