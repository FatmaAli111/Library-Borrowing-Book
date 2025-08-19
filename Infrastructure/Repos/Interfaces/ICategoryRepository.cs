using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<Book>> GetBooksByCategoryId(int categoryId);
    }

}
