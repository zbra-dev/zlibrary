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

        public async Task<Publisher> FindById(long id)
        {
            return await publisherRepository.FindById(id);
        }

         public async Task Delete(long id)
        {
            await publisherRepository.Delete(id);
        }

        public async Task<Publisher> FindByName(string name)
        {
            return await publisherRepository.FindByName(name);
        }

        public async Task<long> Create(Publisher publisher)
        {
            return await publisherRepository.Create(publisher);
        }
    }
}