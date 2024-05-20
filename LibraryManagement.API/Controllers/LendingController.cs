using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Lendings;
using LibraryManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LendingController : ControllerBase
{
    private readonly ILendingService _lendingService;

    public LendingController(ILendingService service)
    {
        _lendingService = service;
    }
   
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Lending>>> GetAllCurrentLendings()
    {
        return Ok(_lendingService.GetAllCurrentLendings());
    }
    
    [HttpGet("{memberId}")]
    public async Task<ActionResult<IEnumerable<Lending>>> GetLendingsByMemberId(Guid memberId)
    {
        return Ok(_lendingService.GetLendingsByMemberId(memberId));
    }
    
    [HttpPost("lend-book")]
    public async Task<ActionResult<IEnumerable<Lending>>> LendBook([FromBody] Guid bookId, Guid memberId)
    {
        return Ok(_lendingService.LendBook(bookId, memberId));
    }
    
    [HttpPost("return-book")]
    public async Task<ActionResult<IEnumerable<Lending>>> ReturnBook([FromBody] Guid bookId, Guid memberId)
    {
        _lendingService.ReturnBook(bookId, memberId);
        return Ok();
    }
}