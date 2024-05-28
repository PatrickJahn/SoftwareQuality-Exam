using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LibraryManagement.Core.Books;
using LibraryManagement.Core.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task Add(Book entity)
        {
            await _context.Books.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Edit(Book entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Book Get(Guid id)
        {
            return _context.Books.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public void Remove(Guid id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public Book? Find(Expression<Func<Book, bool>> predicate)
        {
            return _context.Books.SingleOrDefault(predicate);
        }

        public IEnumerable<Book> FindAll(Expression<Func<Book, bool>> predicate)
        {
            return _context.Books.Where(predicate).ToList();
        }
    }
}