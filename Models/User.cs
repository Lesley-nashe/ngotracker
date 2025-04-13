using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace jobtrackerapi.Models;

public class User : IdentityUser
{
    public required string FirstName { get; set; }
    public required string SecondName { get; set; }
    public required string Role { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

}

public class RegisterModel
{
    public required string FirstName { get; set; }
     public required string SecondName { get; set; }
     public required string Role { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class LoginModel
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class LoginResponse 
{
    public bool IsLogged {get; set;}

    public  string?JwtToken{get;set;}

    public string?RefreshToken {get; set;}
}
