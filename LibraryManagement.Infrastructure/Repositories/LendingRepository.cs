﻿using System;
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
            return context.Lendings.ToList();
        }

        public Lending Get(Guid id)
        {
            return context.Lendings.SingleOrDefault(x => x.Id == id);
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
