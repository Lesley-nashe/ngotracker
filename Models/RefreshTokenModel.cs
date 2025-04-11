using System;

namespace ngotracker.Models;

public class RefreshTokenModel
{
    public required string JwtToken {get;set;}

    public required string RefreshToken {get;set;}

}
