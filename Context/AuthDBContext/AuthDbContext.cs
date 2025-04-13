using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ngotracker.Models.AuthModels;

namespace ngotracker.Context;

public class AuthDbContext(DbContextOptions dbContextOptions) : IdentityDbContext(dbContextOptions)
{
    public DbSet<UserModel> Users { get; set; }

}
