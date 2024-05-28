using LibraryManagement.Application.Interfaces;
using LibraryManagement.Core.Books;
using LibraryManagement.Core.Common.Interfaces;

namespace LibraryManagement.Application.Services
{
    public class BookService(IRepository<Book> bookRepository) : IBookService
    {
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await Task.FromResult(bookRepository.GetAll());
        }

        public async Task<IEnumerable<Book>> GetAllAvailableBooks()
        {
            return await Task.FromResult(bookRepository.FindAll(b => b.IsAvailable));
        }

        public async Task<Book?> GetBookById(Guid id)
        {
            return await Task.FromResult(bookRepository.Get(id));
        }

        public async Task<Book?> GetBookByIsbn(string ISBN)
        {
            return await Task.FromResult(bookRepository.Find(b => b.ISBN == ISBN));
        }

        public async Task<Book> AddBook(Book book)
        {
            if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Author) || string.IsNullOrEmpty(book.ISBN))
            {
                throw new ArgumentException("Title, Author, and ISBN are required.");
            }
                
            await bookRepository.Add(book);
            return book;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            var existingBook = bookRepository.Get(book.Id);
            if (existingBook == null)
            {
                throw new KeyNotFoundException("Book not found.");
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.ISBN = book.ISBN;
            existingBook.IsAvailable = book.IsAvailable;

            bookRepository.Edit(existingBook);
            return existingBook;
        }

        public async Task RemoveBook(Guid id)
        {
            var book = bookRepository.Get(id);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found.");
            }

            bookRepository.Remove(id);
            await Task.CompletedTask;
        }

        public async Task BorrowBook(Guid id)
        {
            var book = bookRepository.Get(id);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found.");
            }

            if (!book.IsAvailable)
            {
                throw new InvalidOperationException("Book is already borrowed.");
            }

            book.IsAvailable = false;
            bookRepository.Edit(book);
            await Task.CompletedTask;
        }

        public async Task ReturnBook(Guid id)
        {
            var book = bookRepository.Get(id);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found.");
            }

            if (book.IsAvailable)
            {
                throw new InvalidOperationException("Book is already available.");
            }

            book.IsAvailable = true;
            bookRepository.Edit(book);
            await Task.CompletedTask;
        }
    }
}
