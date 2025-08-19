using Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.IServices
{
    public interface IBorrowBookService
    {
        Task<bool> BorrowBookAsync(BorrowBookDto dto);
        Task<bool> ReturnBookAsync(int checkoutId);
        Task<List<BorrowedBookResponseDto>> GetAllBorrowedBooksAsync();
        Task<List<BorrowedBookResponseDto>> GetBorrowedBooksByUserAsync(string userId);

    }
}
