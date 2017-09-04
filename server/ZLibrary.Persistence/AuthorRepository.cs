using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace ZLibrary.Persistence
{
    public class AuthorRepository: IAuthorRepository
    {
        private readonly ZLibraryContext context;

        public AuthorRepository(ZLibraryContext context)
        {
            this.context = context;
        }

        public async Task<IList<Author>> FindAll()
        {
            return await context.Authors.ToListAsync();
        }

        public Author FindById(long id)
        {
            return context.Authors.Find(id);
        }
    }
}