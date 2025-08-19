using Data.Dtos;
using Data.Dtos.Validators;
using Infrastructure.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.IServices;

namespace Library_Borrowing_Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookservice;

        public BookController(IBookService Bookservice)
        {
            bookservice = Bookservice;
        }

        [HttpGet("GetAvailableBooks")]
        public async Task<IActionResult> GetAvailableBooks()
        {
            var Books = await bookservice.GetAvailableBooksAsync();
            return Ok(Books);
        }
        [HttpGet("GetBookById/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var BookById =await bookservice.GetByIdAsync(id);
            return Ok(BookById);
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromBody] BookDto dto)
        {
            var validationresult = ValidationService.Validate(dto, new BookValidator());
            if (validationresult.Result != null)
                return BadRequest(validationresult);

            await bookservice.AddBookOrBookCopyAsync(dto);

            return Ok("Book added successfully.");
        }

        [HttpDelete("DeleteBook/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            
          var Result = await bookservice.DeleteBookAsync(id);
            return Ok(Result);
        }

        [HttpGet("checkAvailability/{bookId}")]
        public async Task<IActionResult> CheckAvailability(int bookId)
        {
            var isAvailable = await bookservice.IsBookAvailableAsync(bookId);
            return Ok(new { bookId, isAvailable });
        }

    }
}
