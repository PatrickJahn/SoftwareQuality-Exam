using LibraryManagement.Core.Books;
using LibraryManagement.Core.Lendings;
using LibraryManagement.Core.Members;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Lending> Lendings { get; set; }

    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
    }

}