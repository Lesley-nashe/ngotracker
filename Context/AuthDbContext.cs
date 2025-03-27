using System;
using jobtrackerapi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace jobtrackerapi.Context;

public class AuthDbContext(DbContextOptions dbContextOptions) : IdentityDbContext(dbContextOptions)
{
    public DbSet<User> Users { get; set; }

}
