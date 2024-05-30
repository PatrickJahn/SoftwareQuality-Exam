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
    
    
    [HttpPost]
    public async Task<ActionResult<Member>> AddMember([FromBody] Member member)
    {
        if (string.IsNullOrEmpty(member.Email) || string.IsNullOrEmpty(member.Name) || string.IsNullOrEmpty(member.cprNr))
        {
            throw new ArgumentException("Email, Name, and cpr nr. are required.");
        }

        var addMember = await service.AddMember(member);

        return Ok(addMember);
    }
    
    [HttpDelete("{id:guid}")]
    public  async Task<IActionResult> DeleteMember(Guid id)
    {

        await service.DeleteMember(id);
      
        return Ok();
    }
}