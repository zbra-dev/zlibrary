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
    }
}