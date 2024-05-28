using LibraryManagement.Core.Books;
namespace LibraryManagement.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<IEnumerable<Book>> GetAllAvailableBooks();
        Task<Book?> GetBookById(Guid id);
        Task<Book?> GetBookByIsbn(string isbn);
        Task<Book> AddBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task RemoveBook(Guid id);
        Task BorrowBook(Guid id);
        Task ReturnBook(Guid id);
    }
}