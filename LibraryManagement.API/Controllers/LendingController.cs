using LibraryManagement.Application.Interfaces;
using LibraryManagement.Controllers.Dtos;
using LibraryManagement.Core.Lendings;
using LibraryManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LendingController(ILendingService _lendingService) : ControllerBase
{

    
    [HttpGet("current")]
    public ActionResult<IEnumerable<Lending>> GetAllCurrentLendings()
    {
        var lendings = _lendingService.GetAllCurrentLendings();
        return Ok(lendings);
    }

    [HttpGet("overdue")]
    public ActionResult<IEnumerable<Lending>> GetAllLendingsOverdue()
    {
        var lendings = _lendingService.GetAllLendingsOverdue();
        return Ok(lendings);
    }

    [HttpGet("member/{memberId}")]
    public ActionResult<IEnumerable<Lending>> GetLendingsByMemberId(Guid memberId)
    {
        var lendings = _lendingService.GetLendingsByMemberId(memberId);
        return Ok(lendings);
    }

    [HttpGet("book/{bookId}")]
    public ActionResult<IEnumerable<Lending>> GetLendingsByBookId(Guid bookId)
    {
        var lendings = _lendingService.GetLendingsByBookId(bookId);
        return Ok(lendings);
    }


    [HttpPost("lend")]
    public async Task<ActionResult<Lending>> LendBook([FromBody] LendRequest request)
    {
        try
        {
            var lending = await _lendingService.LendBook(request.BookId, request.MemberId);
            return Ok(lending);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


}