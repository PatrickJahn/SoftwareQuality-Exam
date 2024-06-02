using LibraryManagement.Application.Interfaces;
using LibraryManagement.Application.Services;
using LibraryManagement.Core.Books;
using LibraryManagement.Core.Common.Interfaces;
using LibraryManagement.Core.Lendings;
using LibraryManagement.Core.Members;

namespace LibraryManagement.UnitTests;
  public class LendingServiceTests
    {
        private readonly Mock<IRepository<Lending>> _lendingRepositoryMock;
        private readonly Mock<IBookService> _bookServiceMock;
        private readonly Mock<IMemberService> _memberServiceMock;

        private readonly LendingService _lendingService;

        public LendingServiceTests()
        {
            _lendingRepositoryMock = new Mock<IRepository<Lending>>();
            _bookServiceMock = new Mock<IBookService>();
            _memberServiceMock = new Mock<IMemberService>();

            _lendingService = new LendingService(_lendingRepositoryMock.Object, _bookServiceMock.Object, _memberServiceMock.Object);
        }

        [Fact]
        public void GetAllCurrentLendings_ReturnsAllCurrentLendings()
        {
            var lendings = new List<Lending>
            {
                new Lending { Id = Guid.NewGuid(), BookId = Guid.NewGuid(), ReturnedOn = null},
                new Lending { Id = Guid.NewGuid(), BookId = Guid.NewGuid(), ReturnedOn = new DateTime(2000, 01, 01)},
                new Lending { Id = Guid.NewGuid(), BookId = Guid.NewGuid(), ReturnedOn = null},
                new Lending { Id = Guid.NewGuid(), BookId = Guid.NewGuid(), ReturnedOn = new DateTime(2056, 04, 21)}
            };

            _lendingRepositoryMock.Setup(repo => repo.GetAll()).Returns(lendings);

            var result = _lendingService.GetAllCurrentLendings();

            Assert.Equal(2, result.Count());
            Assert.All(result, lending => Assert.Null(lending.ReturnedOn));
        }

        [Fact]
        public void GetAllLendingsOverdue_ReturnsAllOverdueLendings()
        {
            var lendings = new List<Lending>
            {
                new Lending { Id = Guid.NewGuid(), BookId = Guid.NewGuid(), ShouldBeReturnedBefore = DateTime.UtcNow.AddDays(-1) },
                new Lending { Id = Guid.NewGuid(), BookId = Guid.NewGuid(), ShouldBeReturnedBefore = DateTime.UtcNow.AddDays(1) },
                new Lending { Id = Guid.NewGuid(), BookId = Guid.NewGuid(), ShouldBeReturnedBefore = DateTime.UtcNow.AddDays(-10) }
            };

            _lendingRepositoryMock.Setup(repo => repo.GetAll()).Returns(lendings);

            var result = _lendingService.GetAllLendingsOverdue();

            Assert.Equal(2, result.Count());
            Assert.All(result, lending => Assert.True(lending.ShouldBeReturnedBefore < DateTime.UtcNow));
        }

        [Fact]
        public void GetLendingsByMemberId_ReturnsLendingsForMember()
        {
            var memberId = Guid.NewGuid();
            var lendings = new List<Lending>
            {
                new Lending { Id = Guid.NewGuid(), MemberId = memberId },
                new Lending { Id = Guid.NewGuid(), MemberId = Guid.NewGuid() },
                new Lending { Id = Guid.NewGuid(), MemberId = memberId }
            };

            _lendingRepositoryMock.Setup(repo => repo.GetAll()).Returns(lendings);

            var result = _lendingService.GetLendingsByMemberId(memberId);

            Assert.Equal(2, result.Count());
            Assert.All(result, lending => Assert.Equal(memberId, lending.MemberId));
        }

        [Fact]
        public void GetLendingsByBookId_ReturnsLendingsForBook()
        {
            var bookId = Guid.NewGuid();
            var lendings = new List<Lending>
            {
                new Lending { Id = Guid.NewGuid(), BookId = bookId },
                new Lending { Id = Guid.NewGuid(), BookId = Guid.NewGuid() },
                new Lending { Id = Guid.NewGuid(), BookId = bookId }
            };

            _lendingRepositoryMock.Setup(repo => repo.GetAll()).Returns(lendings);

            var result = _lendingService.GetLendingsByBookId(bookId);

            Assert.Equal(2, result.Count());
            Assert.All(result, lending => Assert.Equal(bookId, lending.BookId));
        }

        [Fact]
        public void GetCurrentLendingOfBook_ReturnsCurrentLendingForBook()
        {
            var bookId = Guid.NewGuid();
            var lendings = new List<Lending>
            {
                new Lending { Id = Guid.NewGuid(), BookId = bookId, ReturnedOn = null },
                new Lending { Id = Guid.NewGuid(), BookId = bookId, ReturnedOn = DateTime.UtcNow },
                new Lending { Id = Guid.NewGuid(), BookId = Guid.NewGuid(), ReturnedOn = null }
            };

            _lendingRepositoryMock.Setup(repo => repo.GetAll()).Returns(lendings);

            var result = _lendingService.GetCurrentLendingOfBook(bookId);

            Assert.NotNull(result);
            Assert.Equal(bookId, result.BookId);
            Assert.Null(result.ReturnedOn);
        }

        [Fact]
        public async Task LendBook_ThrowsException_WhenBookNotFound()
        {
            var bookId = Guid.NewGuid();
            var memberId = Guid.NewGuid();

            _bookServiceMock.Setup(service => service.GetBookById(bookId)).ReturnsAsync((Book)null);

            await Assert.ThrowsAsync<Exception>(async () => await _lendingService.LendBook(bookId, memberId));
        }

        [Fact]
        public async Task LendBook_ThrowsException_WhenMemberNotFound()
        {
            var bookId = Guid.NewGuid();
            var memberId = Guid.NewGuid();

            var book = new Book { Id = bookId, IsAvailable = true };
            _bookServiceMock.Setup(service => service.GetBookById(bookId)).ReturnsAsync(book);
            _memberServiceMock.Setup(service => service.GetById(memberId)).Returns((Member)null);

            await Assert.ThrowsAsync<Exception>(async () => await _lendingService.LendBook(bookId, memberId));
        }
        
        [Fact]
        public async Task LendBook_ThrowsException_WhenMemberIsBanned()
        {
            var bookId = Guid.NewGuid();
            var memberId = Guid.NewGuid();

            var book = new Book { Id = bookId, IsAvailable = true };
            var member = new Member { Id = memberId, Banned = true};

            _bookServiceMock.Setup(service => service.GetBookById(bookId)).ReturnsAsync(book);
            _memberServiceMock.Setup(service => service.GetById(memberId)).Returns(member);

            await Assert.ThrowsAsync<Exception>(async () => await _lendingService.LendBook(bookId, memberId));
        }

        [Fact]
        public async Task LendBook_ThrowsException_WhenBookIsNotAvailable()
        {
            var bookId = Guid.NewGuid();
            var memberId = Guid.NewGuid();

            var book = new Book { Id = bookId, IsAvailable = false };
            var member = new Member { Id = memberId };
            _bookServiceMock.Setup(service => service.GetBookById(bookId)).ReturnsAsync(book);
            _memberServiceMock.Setup(service => service.GetById(memberId)).Returns(member);

            await Assert.ThrowsAsync<Exception>(async () => await _lendingService.LendBook(bookId, memberId));
        }

        [Fact]
        public async Task LendBook_ThrowsException_WhenBookAlreadyLentOut()
        {
            var bookId = Guid.NewGuid();
            var memberId = Guid.NewGuid();

            var book = new Book { Id = bookId, IsAvailable = true };
            var member = new Member { Id = memberId };
            var lending = new Lending { BookId = bookId, ReturnedOn = null };
            _bookServiceMock.Setup(service => service.GetBookById(bookId)).ReturnsAsync(book);
            _memberServiceMock.Setup(service => service.GetById(memberId)).Returns(member);
            _lendingRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Lending> { lending });

            await Assert.ThrowsAsync<Exception>(async () => await _lendingService.LendBook(bookId, memberId));
        }

        [Fact]
        public async Task LendBook_SuccessfulLending()
        {
            var bookId = Guid.NewGuid();
            var memberId = Guid.NewGuid();

            var book = new Book { Id = bookId, IsAvailable = true };
            var member = new Member { Id = memberId };
            _bookServiceMock.Setup(service => service.GetBookById(bookId)).ReturnsAsync(book);
            _memberServiceMock.Setup(service => service.GetById(memberId)).Returns(member);
            _lendingRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Lending>());

            var result = await _lendingService.LendBook(bookId, memberId);

            _lendingRepositoryMock.Verify(repo => repo.Add(It.IsAny<Lending>()), Times.Once);
            Assert.Null(result);
        }
}
