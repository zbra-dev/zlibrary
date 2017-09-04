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

        public async Task<Author> FindById(long id)
        {
            return await context.Authors.FindAsync(id);
        }

        public async Task<IList<Author>> FindByName(string name)
        {
            return await context.Authors.Where(a => a.Name.Contains(name)).ToListAsync();
        }

        public async Task Delete(long id)
        {
            context.Authors.Remove(context.Authors.FirstOrDefault(a => a.Id == id));
            await context.SaveChangesAsync();
        }

        public async Task<long> Create(Author author)
        {
            await context.Authors.AddAsync(author);
            await context.SaveChangesAsync();
            await context.Entry(author).ReloadAsync();
        
            return author.Id;
        }
    }
}