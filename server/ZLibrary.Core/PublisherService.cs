using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using ZLibrary.Persistence;

namespace ZLibrary.Core
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            this.publisherRepository = publisherRepository;
        }

        public async Task<IList<Publisher>> FindAll()
        {
            return await publisherRepository.FindAll();
        }

        public Publisher FindById(long id)
        {
			if (id <= 0 )
            {
                return null;
            }
            return publisherRepository.FindById(id);
        }

         public async Task<IList<Publisher>> FindByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new Publisher[0];
            }
            return await publisherRepository.FindByName(name);
        }

        public async Task<Publisher> Save(Publisher publisher)
        {
            return await publisherRepository.Save(publisher);
        }
    }
}