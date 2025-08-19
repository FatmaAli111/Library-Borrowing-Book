using Data.Dtos;
using Data.Entities;
using Infrastructure;
using Infrastructure.Repos.Interfaces;
using Service.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryResponseDto> AddCategory(string name)
        {
            var category = new Category { Name = name };
            await _unitOfWork.Repository<Category>().AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            if (category == null) return false;

            await _unitOfWork.Repository<Category>().DeleteAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<CategoryResponseDto>> GetAllCategories()
        {
            var categories = await _unitOfWork.Repository<Category>().GetAllAsync();

            var categoryDtos = categories.Select(c => new CategoryResponseDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return categoryDtos;
        }

        public async Task<bool> UpdateCategory(int id, string name)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            if (category == null) return false;

            category.Name = name;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<Book>> GetBooksByCategory(int categoryId)
        {
            return await _unitOfWork.CategoryRepository.GetBooksByCategoryId(categoryId);
        }
    }
}
