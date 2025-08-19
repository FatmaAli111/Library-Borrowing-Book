using Data.Dtos;
using Data.Entities;
using Data.Entities.Data.Entities;
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
    public class BorrowBookRepository : GenericRepository<Checkout>, IBorrowBookRepository
    {
        private readonly AppDbContext context;

        public BorrowBookRepository(AppDbContext Context,IBookRepository bookRepository) : base(Context)
        {
            context = Context;
        }


      
    }
}
