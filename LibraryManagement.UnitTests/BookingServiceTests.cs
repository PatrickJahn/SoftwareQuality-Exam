using LibraryManagement.Application.Services;
using LibraryManagement.Core.Books;
using LibraryManagement.Core.Common.Interfaces;
namespace LibraryManagement.UnitTests
{
    public class BookServiceTests
    {
        private readonly Mock<IRepository<Book>> _bookRepositoryMock;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _bookRepositoryMock = new Mock<IRepository<Book>>();
            _bookService = new BookService(_bookRepositoryMock.Object);
        }

        public static IEnumerable<object[]> GetValidBooks()
        {
            yield return new object[] { new Book { Title = "Test Book 1", Author = "Test Author 1", ISBN = "1234567890" } };
            yield return new object[] { new Book { Title = "Test Book 2", Author = "Test Author 2", ISBN = "0987654321" } };
        }

        public static IEnumerable<object[]> GetInvalidBooks()
        {
            yield return new object[] { new Book { Title = "", Author = "Test Author", ISBN = "1234567890" } };
            yield return new object[] { new Book { Title = "Test Book", Author = "", ISBN = "1234567890" } };
            yield return new object[] { new Book { Title = "Test Book", Author = "Test Author", ISBN = "" } };
        }

        [Theory]
        [MemberData(nameof(GetValidBooks))]
        public async Task AddBook_ValidBook_ReturnsBook(Book newBook)
        {
            _bookRepositoryMock.Setup(repo => repo.Add(It.IsAny<Book>())).Returns(Task.CompletedTask);

            var result = await _bookService.AddBook(newBook);

            Assert.Equal(newBook.Title, result.Title);
            Assert.Equal(newBook.Author, result.Author);
            Assert.Equal(newBook.ISBN, result.ISBN);
            _bookRepositoryMock.Verify(repo => repo.Add(It.IsAny<Book>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetInvalidBooks))]
        public async Task AddBook_InvalidBook_ThrowsArgumentException(Book newBook)
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _bookService.AddBook(newBook));
        }

        [Fact]
        public void GetAllBooks_ReturnsAllBooks()
        {
            var books = new List<Book>
            {
                new Book { Title = "Book 1", Author = "Author 1", ISBN = "1111111111" },
                new Book { Title = "Book 2", Author = "Author 2", ISBN = "2222222222" }
            };

            _bookRepositoryMock.Setup(repo => repo.GetAll()).Returns(books);

            var result = _bookService.GetAllBooks();

            var enumerable = result.Result.ToList();
            Assert.Equal(2, enumerable.Count);
            Assert.Contains(enumerable, b => b.Title == "Book 1");
            Assert.Contains(enumerable, b => b.Title == "Book 2");
        }

        [Fact]
        public void GetAllAvailableBooks_ReturnsOnlyAvailableBooks()
        {
            var books = new List<Book>
            {
                new Book { Title = "Book 1", Author = "Author 1", ISBN = "1111111111", IsAvailable = true },
                new Book { Title = "Book 2", Author = "Author 2", ISBN = "2222222222", IsAvailable = false },
                new Book { Title = "Book 3", Author = "Author 3", ISBN = "3333333333", IsAvailable = true }
            };

            _bookRepositoryMock.Setup(repo => repo.FindAll(b => b.IsAvailable)).Returns(books.Where(b => b.IsAvailable));

            var result = _bookService.GetAllAvailableBooks();

            var enumerable = result.Result.ToList();
            Assert.Equal(2, enumerable.Count);
            Assert.Contains(enumerable, b => b.Title == "Book 1");
            Assert.Contains(enumerable, b => b.Title == "Book 3");
        }

        [Theory]
        [InlineData("11111111-1111-1111-1111-111111111111")]
        public void GetBookById_ExistingId_ReturnsBook(string id)
        {
            var book = new Book { Title = "Book 1", Author = "Author 1", ISBN = "1111111111" };

            _bookRepositoryMock.Setup(repo => repo.Get(book.Id)).Returns(book);

            var result = _bookService.GetBookById(book.Id);

            Assert.NotNull(result);
            Assert.Equal(book.Title, result.Result.Title);
        }

        [Theory]
        [InlineData("11111111-1111-1111-1111-111111111112")]
        public void GetBookById_NonExistingId_ReturnsNull(string id)
        {
            var bookId = new Guid(id);

            _bookRepositoryMock.Setup(repo => repo.Get(bookId)).Returns((Book)null);

            var result = _bookService.GetBookById(bookId);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("1111111111")]
        public void GetBookByISBN_ExistingISBN_ReturnsBook(string isbn)
        {
            var book = new Book { Title = "Book 1", Author = "Author 1", ISBN = isbn };

            _bookRepositoryMock.Setup(repo => repo.Find(b => b.ISBN == isbn)).Returns(book);

            var result = _bookService.GetBookByIsbn(isbn);

            Assert.NotNull(result);
            Assert.Equal(book.Title, result.Result.Title);
        }

        [Theory]
        [InlineData("1111111112")]
        public void GetBookByISBN_NonExistingISBN_ReturnsNull(string isbn)
        {
            _bookRepositoryMock.Setup(repo => repo.Find(b => b.ISBN == isbn)).Returns((Book)null);

            var result = _bookService.GetBookByIsbn(isbn);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateBook_ExistingBook_ReturnsUpdatedBook()
        {
          
            var existingBook = new Book {  Title = "Old Title", Author = "Old Author", ISBN = "1111111111", IsAvailable = true };
            var updatedBook = new Book {  Title = "New Title", Author = "New Author", ISBN = "2222222222", IsAvailable = false };

            _bookRepositoryMock.Setup(repo => repo.Get(existingBook.Id)).Returns(existingBook);
            _bookRepositoryMock.Setup(repo => repo.Edit(It.IsAny<Book>())).Verifiable();

            var result = await _bookService.UpdateBook(updatedBook);

            Assert.Equal(updatedBook.Title, result.Title);
            Assert.Equal(updatedBook.Author, result.Author);
            Assert.Equal(updatedBook.ISBN, result.ISBN);
            Assert.Equal(updatedBook.IsAvailable, result.IsAvailable);
            _bookRepositoryMock.Verify(repo => repo.Edit(It.IsAny<Book>()), Times.Once);
        }

        [Fact]
        public async Task UpdateBook_NonExistingBook_ThrowsKeyNotFoundException()
        {
          
            var updatedBook = new Book { Title = "New Title", Author = "New Author", ISBN = "2222222222", IsAvailable = false };

            _bookRepositoryMock.Setup(repo => repo.Get(updatedBook.Id)).Returns((Book)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _bookService.UpdateBook(updatedBook));
        }

        [Fact]
        public void RemoveBook_ExistingBook_RemovesBook()
        {
          
            var existingBook = new Book { Title = "Test Book", Author = "Test Author", ISBN = "1111111111", IsAvailable = true };

            _bookRepositoryMock.Setup(repo => repo.Get(existingBook.Id)).Returns(existingBook);
            _bookRepositoryMock.Setup(repo => repo.Remove(existingBook.Id)).Verifiable();

            _bookService.RemoveBook(existingBook.Id);

            _bookRepositoryMock.Verify(repo => repo.Remove(existingBook.Id), Times.Once);
        }

        [Fact]
        public async Task RemoveBook_NonExistingBook_ThrowsKeyNotFoundException()
        {
            var bookId = Guid.NewGuid();

            _bookRepositoryMock.Setup(repo => repo.Get(bookId)).Returns((Book)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _bookService.RemoveBook(bookId));
        }

        [Fact]
        public void BorrowBook_AvailableBook_SetsIsAvailableToFalse()
        {
           
            var book = new Book {  Title = "Test Book", Author = "Test Author", ISBN = "1111111111", IsAvailable = true };

            _bookRepositoryMock.Setup(repo => repo.Get(book.Id)).Returns(book);
            _bookRepositoryMock.Setup(repo => repo.Edit(book)).Verifiable();

            _bookService.BorrowBook(book.Id);

            Assert.False(book.IsAvailable);
            _bookRepositoryMock.Verify(repo => repo.Edit(book), Times.Once);
        }

        [Fact]
        public async Task BorrowBook_NonExistingBook_ThrowsKeyNotFoundException()
        {
            var bookId = Guid.NewGuid();

            _bookRepositoryMock.Setup(repo => repo.Get(bookId)).Returns((Book)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _bookService.BorrowBook(bookId));
        }

        [Fact]
        public async Task BorrowBook_AlreadyBorrowedBook_ThrowsInvalidOperationException()
        {
        
            var book = new Book { Title = "Test Book", Author = "Test Author", ISBN = "1111111111", IsAvailable = false };

            _bookRepositoryMock.Setup(repo => repo.Get(book.Id)).Returns(book);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _bookService.BorrowBook(book.Id));
        }

        [Fact]
        public void ReturnBook_BorrowedBook_SetsIsAvailableToTrue()
        {
         
            var book = new Book {  Title = "Test Book", Author = "Test Author", ISBN = "1111111111", IsAvailable = false };

            _bookRepositoryMock.Setup(repo => repo.Get(book.Id)).Returns(book);
            _bookRepositoryMock.Setup(repo => repo.Edit(book)).Verifiable();

            _ = _bookService.ReturnBook(book.Id);

            Assert.True(book.IsAvailable);
            _bookRepositoryMock.Verify(repo => repo.Edit(book), Times.Once);
        }

        [Fact]
        public async Task ReturnBook_NonExistingBook_ThrowsKeyNotFoundException()
        {
            var bookId = Guid.NewGuid();

            _bookRepositoryMock.Setup(repo => repo.Get(bookId)).Returns((Book)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _bookService.ReturnBook(bookId));
        }

        [Fact]
        public async Task ReturnBook_AlreadyAvailableBook_ThrowsInvalidOperationException()
        {
      
            var book = new Book { Title = "Test Book", Author = "Test Author", ISBN = "1111111111", IsAvailable = true };

            _bookRepositoryMock.Setup(repo => repo.Get(book.Id)).Returns(book);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _bookService.ReturnBook(book.Id));
        }
    }
}
