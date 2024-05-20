using LibraryManagement.Core.Lendings;
using LibraryManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LendingController : ControllerBase
{


    public LendingController(LibraryContext context)
    {
    }

   
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Lending>>> GetAllCurrentLending()
    {
        throw new NotImplementedException();
    }
}