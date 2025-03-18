using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shared.DTOs;
using Shared.Models.Entities;

namespace Authentication.API.Infrastructure.Data.Seeders
{
    public class DataSeeder
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration; 

        public DataSeeder(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration; 
        }

        public async Task SeedAsync()
        {
            if (!await _roleManager.RoleExistsAsync("Administrador"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Administrador"));
            }

            if (!await _roleManager.RoleExistsAsync("Usuario"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Usuario"));
            }


            // Leer el administrador desde la configuración
            var adminEmail = _configuration["Admin:Email"];
            var adminPassword = _configuration["Admin:Password"];

            // Verificar si el usuario administrador ya existe
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var user = new Usuario
                {
                    UserName = adminEmail,
                    FirstName = "admin",
                    LastName = "admin",
                    Email = adminEmail
                };

                var result = await _userManager.CreateAsync(user, adminPassword);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Administrador");
                }
            }
        }
    }
}
