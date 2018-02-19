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
        private readonly IAuthorRepository authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public async Task<IList<Author>> FindAll()
        {
            return await authorRepository.FindAll();
        }

        public Author FindById(long id)
        {
            return authorRepository.FindById(id);
        }

        public async Task<IList<Author>> FindByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new Author[0];
            }
            return await authorRepository.FindByName(name);
        }
    }
}