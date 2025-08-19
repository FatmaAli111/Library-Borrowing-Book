using Data.Entities;
using Infrastructure.Context;
using Infrastructure.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetBooksByCategoryId(int categoryId)
        {
            return await _context.Books
                .Where(b => b.CategoryId == categoryId).ToListAsync();
        }
    }

}
