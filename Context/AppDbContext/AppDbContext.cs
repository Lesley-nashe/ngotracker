using System;
using Microsoft.EntityFrameworkCore;
using ngotracker.Models.ApplicationModels;
using ngotracker.Models.GrantModels;
using ngotracker.Models.NgoModels;

namespace ngotracker.Context.AppDbContext;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<NgoModel> NgoModels { get; set; }
    public DbSet<ApplicationModel> ApplicationModels { get; set; }
    public DbSet<GrantModel> GrantModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
