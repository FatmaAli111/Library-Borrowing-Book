using Data.Dtos;
using Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.IServices
{
    public interface IBookService
    {
        Task<List<BookResponseDto>> GetAvailableBooksAsync();
        Task<BookResponseDto> GetByIdAsync(int id);
        Task<string> DeleteBookAsync(int id);
        Task AddBookOrBookCopyAsync(BookDto dto);
        Task<bool> IsBookAvailableAsync(int bookId);
    }
}
