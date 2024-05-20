using LibraryManagement.Core.Lendings;

namespace LibraryManagement.Application.Interfaces;

public interface ILendingService
{
    IEnumerable<Lending> GetAllCurrentLendings();
    IEnumerable<Lending> GetAllLendingsOverdue();
    IEnumerable<Lending> GetLendingsByMemberId(Guid memberId);
    IEnumerable<Lending> GetLendingsByBookId(Guid bookId);
    
    Lending? LendBook(Guid bookId, Guid memberId);
    void ReturnBook(Guid bookId, Guid memberId);
    
}