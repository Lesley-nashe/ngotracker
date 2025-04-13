using System;

namespace ngotracker.Models.AuthModels;

public class RegisterModel
{
     public required string FirstName { get; set; }
     public required string SecondName { get; set; }
     public required string Role { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

}
