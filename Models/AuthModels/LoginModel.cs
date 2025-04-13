using System;

namespace ngotracker.Models.AuthModels;

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
