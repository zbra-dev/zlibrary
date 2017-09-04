using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace ZLibrary.Persistence
{
    public class PublisherRepository: IPublisherRepository
    {
        private readonly ZLibraryContext context;

        public PublisherRepository(ZLibraryContext context)
        {
            this.context = context;
        }

        public async Task<IList<Publisher>> FindAll()
        {
            return await context.Publishers.ToListAsync();
        }

        public async Task<Publisher> FindById(long id)
        {
            return await context.Publishers.FindAsync(id);
        }

        public async Task<Publisher> FindByName(string name)
        {
            return await context.Publishers.FindAsync(name);
        }

        public async Task Delete(long id)
        {
            context.Publishers.Remove(context.Publishers.FirstOrDefault(p => p.Id.Equals(id)));
            await context.SaveChangesAsync();
        }

       public async Task<long> Create(Publisher publisher)
        {
            await context.Publishers.AddAsync(publisher);
            await context.SaveChangesAsync();
            await context.Entry(publisher).ReloadAsync();
            
            return publisher.Id;
        }
    }
}