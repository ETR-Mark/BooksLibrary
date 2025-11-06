using AutoMapper;
using BooksLibrary.Application.DTOs;
using BooksLibrary.Domain.Entities;
using BooksLibrary.Domain.Interfaces.Repositories;
using BooksLibrary.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksLibrary.Application.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IMapper _mapper;

        public BooksService(IBooksRepository booksRepository, IMapper mapper)
        {
            _booksRepository = booksRepository;
            _mapper = mapper;
        }

        public async Task<CreateBookDTO> AddAsync(CreateBookDTO bookDTO)
        {
            var book = _mapper.Map<Book>(bookDTO);
            await _booksRepository.AddAsync(book);
            return _mapper.Map<CreateBookDTO>(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _booksRepository.DeleteAsync(id);
        }

        public async Task<BookDTO> GetBookByIdAsync(int id)
        {
            var book = await _booksRepository.GetByIdAsync(id);
            return _mapper.Map<BookDTO>(book);
        }

        public async Task<List<BookDTO>> GetBooksAsync()
        {
            var books = await _booksRepository.GetAllAsync();
            return _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<BookDTO> UpdateBook(int id, CreateBookDTO bookDto)
        {
            var existingBook = await _booksRepository.GetByIdAsync(id);
            if (existingBook == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }

            existingBook.UpdateDetails(bookDto.Title, bookDto.Description, bookDto.Author);
            existingBook.UpdateStock(bookDto.TotalCopies, bookDto.AvailableCopies);
            
            var updatedBook = await _booksRepository.UpdateAsync(id, existingBook);
            return _mapper.Map<BookDTO>(updatedBook);
        }

        public async Task<BookDTO> AddReviewAsync(int bookId, CreateReviewDTO reviewDto)
        {
            var book = await _booksRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }

            book.AddReview(reviewDto.ReviewerName, reviewDto.Rating, reviewDto.Comment);

            var updatedBook = await _booksRepository.UpdateAsync(bookId, book);
            return _mapper.Map<BookDTO>(updatedBook);
        }
    }
}
