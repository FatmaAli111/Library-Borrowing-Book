using Data.Dtos;
using Data.Entities;
using Data.Entities.Enum;
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
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly AppDbContext context;

        public BookRepository(AppDbContext Context) : base(Context)
        {
            context = Context;
        }

        public async Task<List<Book>> GetAvailableBooks()
        {
            var AvailableBooks =await context.Books.Include(x => x.BookCopies).Include(x => x.Category)
                .Where(x => x.BookCopies.Any(bc => bc.Status == BookCopyStatus.Available)).ToListAsync();

            return  AvailableBooks;

        }

        public Task<Book> GetByIdWithCategory(int id)
        {
            var BookByid = context.Books.Include(x=>x.Category).FirstOrDefaultAsync(x=>x.Id==id);
            return BookByid;

        }
        }
}
