using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using jobtrackerapi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

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

    public string GenerateJwtToken(LoginModel loginuser)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, loginuser.Email),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<IdentityUser> GetUser(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is null)
        {
            return null;
        }
         return user;
    }
}
