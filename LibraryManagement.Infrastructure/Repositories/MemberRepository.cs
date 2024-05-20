using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Core.Common.Interfaces;
using LibraryManagement.Core.Members;
using LibraryManagement.Infrastructure;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class MemberRepository(LibraryContext context) : IRepository<Member>
    {
        public void Add(Member entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Member entity)
        {
            throw new NotImplementedException();
        }

        public Member Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Member> GetAll()
        {
            return context.Members.ToList();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
