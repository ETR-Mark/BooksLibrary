using BooksLibrary.Domain.Entities;
using BooksLibrary.Domain.Interfaces.Repositories;
using BooksLibrary.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Infrastructure.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BooksLibraryDbContext _dbContext;
        public BooksRepository(BooksLibraryDbContext dbContext) { 
            _dbContext = dbContext;
        }

        public async Task<Book> AddAsync(Book book)
        {
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID {id} not found.");

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAllAsync() => await _dbContext.Books.Include(b => b.Reviews).ToListAsync();

        public async Task<Book?> GetByIdAsync(int id) => await _dbContext.Books.Include(b => b.Reviews).FirstOrDefaultAsync(b => b.Id == id);

        public async Task<Book> UpdateAsync(int id, Book book)
        {
            var existingBook = await _dbContext.Books.FindAsync(id);
            if (existingBook == null) throw new Exception("Book not found");

            existingBook.UpdateDetails(book.Title, book.Description, book.Author);
            existingBook.UpdateStock(book.TotalCopies, book.AvailableCopies);
            
            await _dbContext.SaveChangesAsync();
            return existingBook;
        }
    }
}
