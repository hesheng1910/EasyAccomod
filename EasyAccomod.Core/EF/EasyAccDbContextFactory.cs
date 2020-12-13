using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasyAccomod.Core.EF
{
    public class EasyAccDbContextFactory : IDesignTimeDbContextFactory<EasyAccDbContext>
    {
        public EasyAccDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("EasyAccomodDb");

            var optionsBuilder = new DbContextOptionsBuilder<EasyAccDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new EasyAccDbContext(optionsBuilder.Options);
        }
    }
}
