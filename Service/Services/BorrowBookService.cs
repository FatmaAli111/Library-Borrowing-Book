using Data.Dtos;
using Data.Entities.Data.Entities;
using Data.Entities.Enum;
using Data.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Service.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BorrowBookService:IBorrowBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BorrowBookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> BorrowBookAsync(BorrowBookDto dto)
        {
            var bookCopy = await _unitOfWork.Repository<BookCopy>().GetTableAsTracking()
                                .FirstOrDefaultAsync(b => b.Id == dto.BookCopyId
                                && b.Status == BookCopyStatus.Available);

            if (bookCopy == null)
                return false;

            var checkout = new Checkout
            {
                BookCopyID = dto.BookCopyId,
                UserID = dto.UserId,
                CheckoutDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(14), 
                FineAmount = 0,
                IsOverdue = false
            };

            bookCopy.Status = BookCopyStatus.Borrowed;

            await _unitOfWork.Repository<Checkout>().AddAsync(checkout);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ReturnBookAsync(int checkoutId)
        {
            var checkout = await _unitOfWork.Repository<Checkout>().GetTableAsTracking()
                                .Include(c => c.BookCopy)
                                .FirstOrDefaultAsync(c => c.CheckoutID == checkoutId);

            if (checkout == null || checkout.ReturnDate != null)
                return false;

            checkout.ReturnDate = DateTime.UtcNow;
            checkout.IsOverdue = checkout.DueDate < DateTime.UtcNow;

            checkout.BookCopy.Status = BookCopyStatus.Available;

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<BorrowedBookResponseDto>> GetAllBorrowedBooksAsync()
        {
            var checkouts = await _unitOfWork.Repository<Checkout>().GetTableNoTracking()
                                .Where(c => c.ReturnDate == null)
                                .Include(c => c.BookCopy)
                                    .ThenInclude(bc => bc.Book)
                                .Include(c => c.User)
                                .ToListAsync();

            return checkouts.Select(c => new BorrowedBookResponseDto
            {
                BookTitle = c.BookCopy.Book.Title,
                UserName = c.User.UserName,
                CheckoutDate = c.CheckoutDate,
                DueDate = c.DueDate
            }).ToList();
        }

        public async Task<List<BorrowedBookResponseDto>> GetBorrowedBooksByUserAsync(string userId)
        {
            var checkouts = await _unitOfWork.Repository<Checkout>().GetTableNoTracking()
                                .Where(c => c.UserID == userId && c.ReturnDate == null)
                                .Include(c => c.BookCopy)
                                    .ThenInclude(bc => bc.Book)
                                .ToListAsync();

            return checkouts.Select(c => new BorrowedBookResponseDto
            {
                BookTitle = c.BookCopy.Book.Title,
                UserName = c.User.UserName,
                CheckoutDate = c.CheckoutDate,
                DueDate = c.DueDate
            }).ToList();
        }
    }

}
