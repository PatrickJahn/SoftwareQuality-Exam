using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Core.Books;
using LibraryManagement.Core.Common.Interfaces;
using LibraryManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepository(LibraryContext context) : IRepository<Book>
    {
        public void Add(Book entity)
        {
            context.Books.Add(entity);
            context.SaveChanges();
        }

        public void Edit(Book entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Book Get(Guid id)
        {
            return context.Books.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Book> GetAll()
        {
            return context.Books.ToList();
        }

        public void Remove(Guid id)
        {
            var book = context.Books.FirstOrDefault(b => b.Id == id);
            context.Books.Remove(book);
            context.SaveChanges();
        }

    }
}
