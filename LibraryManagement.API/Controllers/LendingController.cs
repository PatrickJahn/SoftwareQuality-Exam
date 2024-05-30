using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Lendings;
using LibraryManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LendingController(ILendingService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Lending>>> GetAllCurrentLendings()
    {
        return Ok(service.GetAllCurrentLendings());
    }
    
    [HttpGet("{memberId}")]
    public async Task<ActionResult<IEnumerable<Lending>>> GetLendingsByMemberId(Guid memberId)
    {
        return Ok(service.GetLendingsByMemberId(memberId));
    }
    
    [HttpPost("lend-book")]
    public async Task<ActionResult<IEnumerable<Lending>>> LendBook([FromBody] Guid bookId, Guid memberId)
    {
        return Ok(service.LendBook(bookId, memberId));
    }
    

}