using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace News.Data.EF
{
    class NewsDbContextFactory : IDesignTimeDbContextFactory<NewsDbContext>
    {
        public NewsDbContext CreateDbContext(string[] args)
        {
            //SetBasePath
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            var connectionString = configuration.GetConnectionString("NewsDbConnection");
            //
            var optionsBuilder = new DbContextOptionsBuilder<NewsDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new NewsDbContext(optionsBuilder.Options);
        }
    }
}
