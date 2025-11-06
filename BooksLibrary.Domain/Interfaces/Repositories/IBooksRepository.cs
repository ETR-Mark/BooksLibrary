using BooksLibrary.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksLibrary.Domain.Interfaces.Repositories
{
    public interface IBooksRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book> AddAsync(Book book);
        Task<Book> UpdateAsync(int id, Book book);
        Task DeleteAsync(int id);
    }
}
