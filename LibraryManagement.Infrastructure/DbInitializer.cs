using LibraryManagement.Infrastructure;
using LibraryManagement.Core.Books;
using LibraryManagement.Core.Lendings;
using LibraryManagement.Core.Members;
using LibraryManagement.Infrastructure.Interfaces;

namespace LibraryManagement.Infrastructure;

public class DbInitializer : IDbInitializer
{
    // This method will create and seed the database.
    public void Initialize(LibraryContext context)
    {
        // Delete the database, if it already exists. I do this because an
        // existing database may not be compatible with the entity model,
        // if the entity model was changed since the database was created.
        context.Database.EnsureDeleted();

        // Create the database, if it does not already exists. This operation
        // is necessary, if you dont't use the in-memory database.
        context.Database.EnsureCreated();

        // Look for any books.
        if (context.Books.Any())
        {
            return;   // DB has been seeded
        }


        var bookId = Guid.NewGuid();
        List<Book> customers = new List<Book>
        {
            new Book() { IsAvailable= true, ISBN= "12345678", Author = "Hans Christian", Title = "Den flyvende gryde",MaxNumberOfLendingDays = 14},
            new Book() { IsAvailable= true, ISBN= "13575321", Author = "Hans Christian", Title = "De tre små grise",  MaxNumberOfLendingDays = 14},
            new Book() { IsAvailable= true, ISBN= "87654321", Author = "Hans Christian", Title = "Isdronningen",  MaxNumberOfLendingDays = 21},
            new Book() { IsAvailable= true, ISBN= "12784671", Author = "Hans Christian", Title = "Den grimme ælling",   MaxNumberOfLendingDays = 31},
            new Book() { IsAvailable= false, ISBN= "12415222", Author = "Julie Mansen", Title = "Pigen med tændstikkerne", MaxNumberOfLendingDays = 31},

        };

        var memberGuid = Guid.NewGuid();
        List<Member> members = new List<Member>
        {
            new Member { Email = "first@member.dk", Name = "Member1", Id = memberGuid, CprNr = "123121"}
        };
        
        List<Lending> lendings = new List<Lending>
        {
            new Lending() { Id= Guid.NewGuid(), BookId = bookId, MemberId = memberGuid, IssuedOn = new DateTime(2024,05,09), ShouldBeReturnedBefore = new DateTime(2024,06,09), },
        };

        DateTime date = DateTime.Today.AddDays(4);
       

        context.Books.AddRange(customers);
        context.Members.AddRange(members);
        context.SaveChanges();
        context.Lendings.AddRange(lendings);
        context.SaveChanges();
    }
}
