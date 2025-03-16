using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Authentication.API.Infrastructure.Data.Context;
using System.IO;

namespace Authentication.API.Infrastructure.Data.Factories
{
    public class MyIdentityDbContextFactory : IDesignTimeDbContextFactory<MyIdentityDBContext>
    {
        public MyIdentityDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyIdentityDBContext>();

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("AutenticacionConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new MyIdentityDBContext(optionsBuilder.Options);
        }
    }
}
