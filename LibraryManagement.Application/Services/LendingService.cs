using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Books;
using LibraryManagement.Core.Common.Interfaces;
using LibraryManagement.Core.Lendings;

namespace LibraryManagement.Application.Services;

public class LendingService: ILendingService
{
    
    private readonly IRepository<Lending> _lendingRepository;

    public LendingService(IRepository<Lending> lendingRepository)
    {
        _lendingRepository = lendingRepository;
    }
    
    public IEnumerable<Lending> GetAllCurrentLendings()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Lending> GetAllLendingsOverdue()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Lending> GetLendingsByMemberId(Guid memberId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Lending> GetLendingsByBookId(Guid bookId)
    {
        throw new NotImplementedException();
    }

    public Lending? LendBook(Guid bookId, Guid memberId)
    {
        throw new NotImplementedException();
    }

    public void ReturnBook(Guid bookId, Guid memberId)
    {
        throw new NotImplementedException();
    }
}