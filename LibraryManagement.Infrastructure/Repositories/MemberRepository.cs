using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Core.Common.Interfaces;
using LibraryManagement.Core.Members;
using LibraryManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class MemberRepository(LibraryContext context) : IRepository<Member>
    {
        public void Add(Member entity)
        {
            context.Members.Add(entity);
            context.SaveChanges();
        
        }

        public void Edit(Member entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Member Get(Guid id)
        {
            return context.Members.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Member> GetAll()
        {
            return context.Members.ToList();
        }

        public void Remove(Guid id)
        {
            var member = context.Members.FirstOrDefault(b => b.Id == id);
            context.Members.Remove(member);
            context.SaveChanges();
        }
    }
}
