using LibraryManagement.Core.Books;
using LibraryManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly LibraryContext _context;

    public BooksController(LibraryContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        return await _context.Books.ToListAsync();
    }
}