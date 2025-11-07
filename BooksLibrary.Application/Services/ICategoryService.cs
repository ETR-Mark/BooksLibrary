using BooksLibrary.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Application.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO?> GetCategoryByIdAsync(int id);
        Task<CreateCategoryDTO> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO);
        Task<CategoryDTO> UpdateCategoryAsync(int id, CreateCategoryDTO updateCategoryDTO);
        Task DeleteCategoryAsync(int id);
        Task<CategoryDTO> AddBookAsync(int id, CreateBookDTO createBookDTO);
    }
}
