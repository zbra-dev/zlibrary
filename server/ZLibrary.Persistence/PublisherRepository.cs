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

        public Publisher FindById(long id)
        {
            return context.Publishers.Find(id);
        }

         public async Task<IList<Publisher>> FindByName(string name)
        {
            var publishers = await context.Publishers
               .Where(a => a.Name.Contains(name))
               .ToListAsync();

            return publishers;
        }

        public async Task<Publisher> Save(Publisher publisher)
        {
            if (!ExistsOther(publisher))
            {

                if (publisher.Id <= 0)
                {
                    context.Publishers.Add(publisher);
                }
                else
                {
                    context.Publishers.Update(publisher);
                }

            }
            else
            {
                throw new Exception("Editora já existe.");
            }

            await context.SaveChangesAsync();
            await context.Entry(publisher).ReloadAsync();
            return publisher;
        }

        private bool ExistsOther(Publisher publisher)
        {
            return context.Publishers
                .Where(p => p.Name == publisher.Name)
                .Where(p => p.Id != publisher.Id)
                .Any();
        }
    
    }
}