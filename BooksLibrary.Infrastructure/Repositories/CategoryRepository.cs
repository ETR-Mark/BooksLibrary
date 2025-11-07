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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BooksLibraryDbContext _dbContext;
        public CategoryRepository(BooksLibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> AddAsync(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null)
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            _dbContext.Categories.Remove(category);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await _dbContext.Categories.Include(c => c.Books).ToListAsync();
            return categories;
        }

        public Task<Category?> GetByIdAsync(int id)
        {
            var category = _dbContext.Categories.Include(c => c.Books).FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Category> UpdateAsync(int id, Category category)
        {
            var existingCategory = await _dbContext.Categories.FindAsync(id);
            if (existingCategory == null) throw new Exception("Category not found");
            existingCategory.UpdateDetails(category.Name, category.Description);
            await _dbContext.SaveChangesAsync();
            return existingCategory;
        }
    }
}
