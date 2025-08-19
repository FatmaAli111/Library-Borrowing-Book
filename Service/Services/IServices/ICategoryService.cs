using Data.Dtos;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.IServices
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> GetAllCategories();
        Task<CategoryResponseDto> AddCategory(string name);
        Task<bool> DeleteCategory(int id);
        Task<bool> UpdateCategory(int id,string name);
        Task<List<Book>> GetBooksByCategory(int categoryId);
    }

}
