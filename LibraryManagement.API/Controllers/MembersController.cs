using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Members;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;


[Route("api/[controller]")]
[ApiController]
public class MembersController : ControllerBase
{

    private readonly IMemberService _memberService;

    public MembersController(IMemberService service)
    {
        _memberService = service;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Member>>> GetAll()
    {
       return Ok(_memberService.GetAllMembers());
    }  
}