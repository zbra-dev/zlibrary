using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using ZLibrary.Persistence;

namespace ZLibrary.Core
{
    public class AuthorService : IAuthorService
    {
        private IAuthorRepository authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public async Task<IList<Author>> FindAll()
        {
            return await authorRepository.FindAll();
        }

        public async Task<Author> FindById(long id)
        {
            return await authorRepository.FindById(id);
        }

        public async Task<IList<Author>> FindByName(string name)
        {
            return await authorRepository.FindByName(name);
        }

        public async Task Delete(long id)
        {
            await authorRepository.Delete(id);
        }

          public async Task<long> Create(Author author)
        {
            return await authorRepository.Create(author);
        }
    }
}