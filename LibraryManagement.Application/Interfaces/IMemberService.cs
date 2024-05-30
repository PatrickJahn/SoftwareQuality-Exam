using LibraryManagement.Core.Members;

namespace LibraryManagement.Application.Interfaces;

public interface IMemberService
{

    IEnumerable<Member> GetAllMembers();
    Member GetById(Guid id);
    Task<Member> AddMember(Member member);
    Task DeleteMember(Guid id);

}