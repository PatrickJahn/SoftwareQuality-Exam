using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LibraryManagement.Infrastructure;
using LibraryManagement.Core.Common.Interfaces;
using LibraryManagement.Core.Lendings;
using LibraryManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class LendingRepository(LibraryContext context) : IRepository<Lending>
    {
        public IEnumerable<Lending> GetAll()
        {
            return context.Lendings.ToList();
        }

        public Lending Get(Guid id)
        {
            return context.Lendings.SingleOrDefault(x => x.Id == id);
        }

        public async Task Add(Lending entity)
        {
            context.Lendings.Add(entity);
            context.SaveChanges();        }

        public void Edit(Lending entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Remove(Guid id)
        {
            var lending = context.Lendings.FirstOrDefault(b => b.Id == id);
            context.Lendings.Remove(lending);
            context.SaveChanges();
        }

        public Lending? Find(Expression<Func<Lending, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lending> FindAll(Expression<Func<Lending, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
