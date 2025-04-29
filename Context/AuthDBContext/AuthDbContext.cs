using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ngotracker.Models.AuthModels;

namespace ngotracker.Context;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : IdentityDbContext(options)
{
    public DbSet<UserModel> Users { get; set; }

}
