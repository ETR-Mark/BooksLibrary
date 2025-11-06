using BooksLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Infrastructure.Persistence
{
    public class BooksLibraryDbContext : DbContext
    {
        public BooksLibraryDbContext(DbContextOptions options): base(options) { }
        public DbSet<Book> Books { get; set; } = null!;
    }
}
