using jobtrackerapi.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Action<DbContextOptionsBuilder> commonOptions = (options) => 
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddDbContext<AuthDbContext>(commonOptions);

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<AuthDbContext>()
.AddDefaultTokenProviders().AddDefaultUI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();
