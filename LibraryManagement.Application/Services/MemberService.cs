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

    public Member AddMember(Member member)
    {
        throw new NotImplementedException();
    }

    public Member DeleteMember(Guid id)
    {
        throw new NotImplementedException();
    }
}