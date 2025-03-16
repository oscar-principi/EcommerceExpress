using Authentication.API.Infrastructure.Data.Context;
using Authentication.API.Infrastructure.Data.Seeders;
using Authentication.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Models.Entities;
using System;
using System.Text;

Console.WriteLine("Antes de crear la app...");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<AuthenticationServices>();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
            };
        });

builder.Services.AddControllers();
builder.Services.AddDbContext<MyIdentityDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AutenticacionConnection")));

builder.Services
    .AddIdentity<Usuario, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true; 
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;  
        options.Password.RequiredLength = 8;

        options.SignIn.RequireConfirmedEmail = false; 

        options.Lockout.AllowedForNewUsers = true;
        options.Lockout.MaxFailedAccessAttempts = 5;        
    })
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<MyIdentityDBContext>();


builder.Services.AddScoped<DataSeeder>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Modo desarrollo iniciado...");
    using (var scope = app.Services.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        await seeder.SeedAsync();
    }
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

