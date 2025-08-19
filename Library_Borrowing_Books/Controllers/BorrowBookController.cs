using Data.Dtos;
using Infrastructure.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.IServices;

namespace Library_Borrowing_Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowBookController : ControllerBase
    {
        private readonly IBorrowBookService borrowBookService;

        public BorrowBookController(IBorrowBookService BorrowBookService)
        {
            borrowBookService = BorrowBookService;
        }
        [HttpPost("BorrowBook")]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowBookDto dto)
        {
            var validationresult = ValidationService.Validate(dto, new BorrowBookValidator());

            if (validationresult.Result != null)
                return BadRequest(validationresult);

            var result = await borrowBookService.BorrowBookAsync(dto);
            if (!result)
                return BadRequest("Book is not available or something went wrong.");

            return Ok("Book borrowed successfully.");
        }

        [HttpPost("ReturnBook/{checkoutId}")]
        public async Task<IActionResult> ReturnBook(int checkoutId)
        {
            var result = await borrowBookService.ReturnBookAsync(checkoutId);
            if (!result)
                return BadRequest("Could not return book.");

            return Ok("Book returned successfully.");
        }

        [HttpGet("GetAllBorrowedBooks")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> GetAllBorrowedBooks()
        {
            var borrowedBooks = await borrowBookService.GetAllBorrowedBooksAsync();
            return Ok(borrowedBooks);
        }

        [HttpGet("GetBorrowedBooksByUser/{userId}")]
        [Authorize(Roles = "Admin,User")] 
        public async Task<IActionResult> GetBorrowedBooksByUser(string userId)
        {
            var books = await borrowBookService.GetBorrowedBooksByUserAsync(userId);
            return Ok(books);
        }

    }
}
