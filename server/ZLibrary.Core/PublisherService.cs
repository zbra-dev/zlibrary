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
        private IPublisherRepository publisherRepository;

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
            return publisherRepository.FindById(id);
        }
    }
}