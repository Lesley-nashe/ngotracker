using System;

namespace ngotracker.Models.AuthModels;

public class RefreshTokenModel
{
    public required string JwtToken {get;set;}

    public required string RefreshToken {get;set;}

}
