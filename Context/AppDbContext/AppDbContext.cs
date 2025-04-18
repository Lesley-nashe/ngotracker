using System;
using Microsoft.EntityFrameworkCore;
using ngotracker.Models.NgoModels;

namespace ngotracker.Context.AppDbContext;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<NgoModel> NgoModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
    }

}
