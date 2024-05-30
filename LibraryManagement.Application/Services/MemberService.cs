using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Common.Interfaces;
using LibraryManagement.Core.Lendings;
using LibraryManagement.Core.Members;

namespace LibraryManagement.Application.Services;

public class MemberService : IMemberService
{
    
    private readonly IRepository<Member> _membersRepository;

    public MemberService(IRepository<Member> membersRepository)
    {
        _membersRepository = membersRepository;
    }
    public IEnumerable<Member> GetAllMembers()
    {
        return _membersRepository.GetAll();
    }

    public Member GetById(Guid id)
    {
        var member = _membersRepository.Get(id);
        if (member is null)
            throw new Exception("Member not found");

        return member;
    }

    public async Task<Member> AddMember(Member member)
    {
        if (string.IsNullOrEmpty(member.Email) || string.IsNullOrEmpty(member.Name) || string.IsNullOrEmpty(member.cprNr))
        {
            throw new ArgumentException("Email, Name, and cpr nr. are required.");
        }
                
        await _membersRepository.Add(member);
        
        return member;
    }

    public async Task DeleteMember(Guid id)
    {
        var member = _membersRepository.Get(id);
        if (member == null)
        {
            throw new KeyNotFoundException("Member not found.");
        }

        _membersRepository.Remove(id);
        await Task.CompletedTask;
    }
}