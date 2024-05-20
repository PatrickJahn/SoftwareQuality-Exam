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
}