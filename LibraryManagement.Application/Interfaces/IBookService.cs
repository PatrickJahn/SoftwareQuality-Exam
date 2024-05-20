using LibraryManagement.Core.Books;

namespace LibraryManagement.Application.Interfaces;

public interface IBookService
{
    IEnumerable<Book> GetAllBooks();
    IEnumerable<Book> GetAllAvailableBooks();
    Book? GetBookById(Guid id);
    Book? GetBookByISBN(string ISBN);

}