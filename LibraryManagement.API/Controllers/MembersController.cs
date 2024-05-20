using LibraryManagement.Core.Members;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;


[Route("api/[controller]")]
[ApiController]
public class MembersController : ControllerBase
{

    public MembersController()
    {
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Member>>> GetAll()
    {
        throw new NotImplementedException();
    }  
}