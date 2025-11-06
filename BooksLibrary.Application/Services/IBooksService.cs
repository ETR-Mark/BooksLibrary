using BooksLibrary.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksLibrary.Domain.Interfaces.Services
{
    public interface IBooksService
    {
        Task<List<BookDTO>> GetBooksAsync();
        Task<BookDTO> GetBookByIdAsync(int id);
        Task<CreateBookDTO> AddAsync(CreateBookDTO bookDto);
        Task<BookDTO> UpdateBook(int id, CreateBookDTO bookDto);
        Task DeleteBookAsync(int id);
    }
}
