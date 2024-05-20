using LibraryManagement.Core.Members;

namespace LibraryManagement.Application.Interfaces;

public interface IMemberService
{

    IEnumerable<Member> GetAllMembers();
    Member GetById(Guid id);
    Member AddMember(Member member);
    Member DeleteMember(Guid id);

}