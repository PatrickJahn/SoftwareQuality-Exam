using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Books;
using LibraryManagement.Core.Common.Interfaces;
using LibraryManagement.Core.Lendings;

namespace LibraryManagement.Application.Services;

public class LendingService: ILendingService
{
    
    private readonly IRepository<Lending> _lendingRepository;
    private readonly IBookService _bookService;
    private readonly IMemberService _memberService;

    public LendingService(IRepository<Lending> lendingRepository, IBookService bookService, IMemberService memberService)
    {
        _lendingRepository = lendingRepository;
        _bookService = bookService;
        _memberService = memberService;
    }
    
    public IEnumerable<Lending> GetAllCurrentLendings()
    {
        return _lendingRepository.GetAll().Where(x => x.ReturnedOn == null);
    }

    public IEnumerable<Lending> GetAllLendingsOverdue()
    {
        return _lendingRepository.GetAll().Where(x => x.ShouldBeReturnedBefore < DateTime.UtcNow);
    }

    public IEnumerable<Lending> GetLendingsByMemberId(Guid memberId)
    {
        return _lendingRepository.GetAll().Where(x => x.MemberId == memberId);
    }

    public IEnumerable<Lending> GetLendingsByBookId(Guid bookId)
    {
       return _lendingRepository.GetAll().Where(x => x.BookId == bookId);
        
    }
    
    public Lending? GetCurrentLendingOfBook(Guid bookId)
    {
       return _lendingRepository.GetAll().SingleOrDefault(x => x.BookId == bookId && x.ReturnedOn == null);
    }

    public async Task<Lending?>  LendBook(Guid bookId, Guid memberId)
    {
        
        // Check if book exists 
        var book = await _bookService.GetBookById(bookId);

        if (book == null)
        {
            throw new Exception("Book not found");
        }
        
        // Check if member exists 
        var member = _memberService.GetById(memberId);

        if (member == null)
        {
            throw new Exception("Member not found");
        }
        
        // Check if book is already lend 
        var lending = GetCurrentLendingOfBook(bookId);
        var bookAlreadyLendOut = lending != null && lending.ReturnedOn == null;

        if (!book.IsAvailable || bookAlreadyLendOut || member.Banned)
        {
            throw new Exception("Could not lend book");
        } 
        
        await _lendingRepository.Add(new Lending()
        {
            Id = Guid.NewGuid(),
            BookId = bookId,
            MemberId = memberId,
            IssuedOn = DateTime.UtcNow
        });

        return lending;
    }


}