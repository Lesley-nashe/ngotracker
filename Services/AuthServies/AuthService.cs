using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ngotracker.Models.AuthModels;

namespace jobtrackerapi.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<UserModel> _userManager = null!;
    private readonly SignInManager<UserModel> _signInManager = null!;
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
    {
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task<LoginResponse> LoginUser(LoginModel model)
    {
        LoginResponse response = new();
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is null || await _userManager.CheckPasswordAsync(user, model.Password) == false)
        {
            return response;
        }

        response.IsLogged = true;
        response.JwtToken = GenerateJwtToken(model.Email);
        response.RefreshToken = GenerateRefreshToken();

        user.RefreshToken = response.RefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(12);
        await _userManager.UpdateAsync(user);

        return response;
    }

    public async Task<bool> RegisterUser(RegisterModel model)
    {
        var IdentityUser = new UserModel
        {
            FirstName = model.FirstName,
            SecondName = model.SecondName,
            Role = model.Role,
            UserName = model.Email,
            Email = model.Email,
        };

        var result = await _userManager.CreateAsync(IdentityUser, model.Password);

        Console.WriteLine(result);

        return result.Succeeded;
    }

    public string GenerateJwtToken(string userName)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddSeconds(60),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<LoginResponse> RefreshToken(RefreshTokenModel model)
    {
        var principal = GetTokenPrincipal(model.JwtToken);

        var response = new LoginResponse();

        if (principal?.Identity?.Name is null) return response;

        var IdentityUser = await _userManager.FindByNameAsync(principal.Identity.Name);

        if (
        IdentityUser is null ||
        IdentityUser.RefreshToken != model.RefreshToken ||
        IdentityUser.RefreshTokenExpiryTime < DateTime.Now) return response;

        response.IsLogged = true;
        response.JwtToken = GenerateJwtToken(principal.Identity.Name);
        response.RefreshToken = GenerateRefreshToken();

        IdentityUser.RefreshToken = response.RefreshToken;
        IdentityUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(12);
        await _userManager.UpdateAsync(IdentityUser);

        return response;
    }

    private ClaimsPrincipal? GetTokenPrincipal(string token)
    {
        var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value!));

        var validation = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateActor = false,
            ValidateLifetime = false,
            ValidateAudience = false,
            IssuerSigningKey = securitykey
        };
        return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
    }

    public async Task<UserModel> GetUser(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is null)
        {
            return null;
        }
        return user;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];

        using (var numberGenerator = RandomNumberGenerator.Create())
        {
            numberGenerator.GetBytes(randomNumber);
        }
        return Convert.ToBase64String(randomNumber);
    }
}
