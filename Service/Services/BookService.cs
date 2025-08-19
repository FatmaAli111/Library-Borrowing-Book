using Data.Dtos;
using Data.Entities;
using Data.Entities.Data.Entities;
using Data.Entities.Enum;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Service.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Service.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<BookResponseDto>> GetAvailableBooksAsync()
        {
            var Books =await unitOfWork.BookRepository.GetAvailableBooks();
            var result = Books.Select(b => new BookResponseDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                CategoryName =b.Category.Name
            }).ToList();

            return result;

        }
        public async Task<BookResponseDto> GetByIdAsync(int id)
        {
            var Book= await unitOfWork.BookRepository.GetByIdWithCategory(id);


            if (Book == null)
                return null;

            var result = new BookResponseDto
            {
                Id = Book.Id,
                Title = Book.Title,
                Author = Book.Author,
                ISBN = Book.ISBN,
                CategoryName = Book.Category.Name
            };
            return result;

        }

        public async Task<string> DeleteBookAsync(int id)
        {
            var Book =await unitOfWork.Repository<Book>().GetByIdAsync(id);
           await unitOfWork.Repository<Book>().DeleteAsync(Book);
            await unitOfWork.SaveChangesAsync();
            return "Deleted Successfully";
        }

        public async Task AddBookOrBookCopyAsync(BookDto dto)
        {
            var category = await unitOfWork.Repository<Category>().GetByIdAsync(dto.CategoryId);
            if (category == null)
                throw new Exception("Category does not exist.");

            var existingBook = await unitOfWork.Repository<Book>()
                .GetTableNoTracking()
                .FirstOrDefaultAsync(b => b.Title == dto.Title && b.ISBN == dto.ISBN);

            int bookId;

            if (existingBook == null)
            {
                var newBook = new Book
                {
                    Title = dto.Title,
                    Author = dto.Author,
                    ISBN = dto.ISBN,
                    CategoryId = dto.CategoryId
                };

                await unitOfWork.Repository<Book>().AddAsync(newBook);
                await unitOfWork.SaveChangesAsync();
                bookId = newBook.Id;
            }
            else
            {
                bookId = existingBook.Id;
            }

            var bookCopy = new BookCopy
            {
                BookId = bookId,
                Status = dto.Status
            };

            await unitOfWork.Repository<BookCopy>().AddAsync(bookCopy);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> IsBookAvailableAsync(int bookId)
        {
            var book = await unitOfWork.Repository<Book>()
                .GetTableNoTracking()
                .Include(b => b.BookCopies)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null) return false;

            return book.BookCopies.Any(copy => copy.Status == BookCopyStatus.Available);
        }

    }
}
