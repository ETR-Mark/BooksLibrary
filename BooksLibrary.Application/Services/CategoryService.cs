using AutoMapper;
using BooksLibrary.Application.DTOs;
using BooksLibrary.Domain.Entities;
using BooksLibrary.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> AddBookAsync(int categoryId, CreateBookDTO createBookDTO)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
            }
            category
                .AddBook(
                    createBookDTO.Title, 
                    createBookDTO.Description, 
                    createBookDTO.Author, 
                    createBookDTO.TotalCopies, 
                    createBookDTO.AvailableCopies
                );
            var updatedCategory = await _categoryRepository.UpdateAsync(categoryId, category);
            return _mapper.Map<CategoryDTO>(updatedCategory);
        }

        public async Task<CreateCategoryDTO> CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            var category = _mapper.Map<Category>(createCategoryDTO);
            await _categoryRepository.AddAsync(category);
            return _mapper.Map<CreateCategoryDTO>(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = _categoryRepository.GetAllAsync();
            return _mapper.Map<Task<List<CategoryDTO>>>(categories);
        }

        public Task<CategoryDTO?> GetCategoryByIdAsync(int id)
        {
            var category = _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<Task<CategoryDTO?>>(category);
        }

        public async Task<CategoryDTO> UpdateCategoryAsync(int id, CreateCategoryDTO updateCategoryDTO)
        {
            var categoryToUpdate = _mapper.Map<Category>(updateCategoryDTO);
            var updatedCategory = await _categoryRepository.UpdateAsync(id, categoryToUpdate);
            return _mapper.Map<CategoryDTO>(updatedCategory);
        }
    }
}
