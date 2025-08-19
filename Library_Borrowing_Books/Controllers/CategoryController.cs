using Data.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Services.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_Borrowing_Books.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] string name)
        {
            var result = await _categoryService.AddCategory(name);
            return Ok(result);
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategories();
            return Ok(result);
        }

        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] string name)
        {
            var success = await _categoryService.UpdateCategory(id, name);
            if (!success) return NotFound("Category not found.");
            return NoContent();
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var success = await _categoryService.DeleteCategory(id);
            if (!success) return NotFound("Category not found.");
            return NoContent();
        }

        [HttpGet("GetBooksByCategory/{categoryId}")]
        public async Task<IActionResult> GetBooksByCategory(int categoryId)
        {
            var books = await _categoryService.GetBooksByCategory(categoryId);
            return Ok(books);
        }
    }
}
