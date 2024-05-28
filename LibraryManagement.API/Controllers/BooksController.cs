using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Books;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(IBookService bookService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return Ok(await bookService.GetAllBooks());
        }

        [HttpGet("available")]
        public async Task <ActionResult<IEnumerable<Book>>> GetAvailableBooks()
        {
            return Ok(await bookService.GetAllAvailableBooks());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Book>> GetBook(Guid id)
        {
            var book = await bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpGet("isbn/{isbn}")]
        public async Task <ActionResult<Book>> GetBookByIsbn(string isbn)
        {
            var book = await bookService.GetBookByIsbn(isbn);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook([FromBody] Book book)
        {
            if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Author) || string.IsNullOrEmpty(book.ISBN))
            {
                return BadRequest("Title, Author, and ISBN are required.");
            }

            var addedBook = await bookService.AddBook(book);

            return CreatedAtAction(nameof(GetBook), new { id = addedBook.Id }, addedBook);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest("Book ID mismatch.");
            }

            try
            {
                await bookService.UpdateBook(book);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult RemoveBook(Guid id)
        {
            try
            {
                bookService.RemoveBook(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("{id:guid}/borrow")]
        public IActionResult BorrowBook(Guid id)
        {
            try
            {
                bookService.BorrowBook(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpPost("{id:guid}/return")]
        public IActionResult ReturnBook(Guid id)
        {
            try
            {
                bookService.ReturnBook(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
