using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BooksLibrary.Infrastructure.Persistence
{
    public class BooksLibraryDbContextFactory : IDesignTimeDbContextFactory<BooksLibraryDbContext>
    {
        public BooksLibraryDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..\\BooksLibrary.API"))
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BooksLibraryDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new BooksLibraryDbContext(optionsBuilder.Options);
        }
    }
}
