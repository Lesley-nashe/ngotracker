using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ngotracker.Models.AuthModels;

public class UserModel : IdentityUser
{
    public required string FirstName { get; set; }
    public required string SecondName { get; set; }
    public required string Role { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

}

