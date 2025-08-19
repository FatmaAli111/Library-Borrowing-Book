using Data.Dtos;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos.Interfaces
{
  public interface IBookRepository:IGenericRepository<Book>
    {
        Task<List<Book>> GetAvailableBooks();
        Task<Book> GetByIdWithCategory(int id);

    }
}
