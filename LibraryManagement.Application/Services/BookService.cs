using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Books;
using LibraryManagement.Core.Common.Interfaces;

namespace LibraryManagement.Application.Services;

public class BookService: IBookService
{

    private readonly IRepository<Book> _bookRepository;

    public BookService(IRepository<Book> bookRepository)
    {
        _bookRepository= bookRepository;
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return _bookRepository.GetAll();
    }

    public IEnumerable<Book> GetAllAvailableBooks()
    {
        throw new NotImplementedException();
    }

    public Book? GetBookById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Book? GetBookByISBN(string ISBN)
    {
        throw new NotImplementedException();
    }
}