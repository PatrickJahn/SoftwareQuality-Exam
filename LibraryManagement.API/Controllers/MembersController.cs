using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Members;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;


[Route("api/[controller]")]
[ApiController]
public class MembersController(IMemberService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Member>>> GetAll()
    {
       return Ok(service.GetAllMembers());
    }  
    
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Member>>> GetById(string id)
    {
        return Ok(service.GetById(Guid.Parse(id)));
    }  
}