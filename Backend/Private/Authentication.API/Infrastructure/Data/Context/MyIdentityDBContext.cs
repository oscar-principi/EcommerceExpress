using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Entities;


namespace Authentication.API.Infrastructure.Data.Context
{
    public class MyIdentityDBContext : IdentityDbContext<Usuario, IdentityRole, string>
    {
        public MyIdentityDBContext(DbContextOptions<MyIdentityDBContext> options) : base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
