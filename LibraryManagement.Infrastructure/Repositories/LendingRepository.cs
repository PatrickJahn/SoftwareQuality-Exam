using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Core.Common.Interfaces;
using LibraryManagement.Core.Lendings;
using LibraryManagement.Infrastructure;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class LendingRepository(LibraryContext context) : IRepository<Lending>
    {
        public IEnumerable<Lending> GetAll()
        {
            throw new NotImplementedException();
        }

        public Lending Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(Lending entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Lending entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
