using LibraryManagement.Core.Lendings;

namespace LibraryManagement.Application.Interfaces;

public interface ILendingService
{
    IEnumerable<Lending> GetAllCurrentLendings();
    IEnumerable<Lending> GetAllLendingsOverdue();
    IEnumerable<Lending> GetLendingsByMemberId(Guid memberId);
    IEnumerable<Lending> GetLendingsByBookId(Guid bookId);
    
    Task<Lending?> LendBook(Guid bookId, Guid memberId);
    
}