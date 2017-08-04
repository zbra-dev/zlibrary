using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using ZLibrary.Persistence;

namespace ZLibrary.Core
{
    public class BookService : IBookService
    {
        private IBookRepository bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<IList<Book>> FindAll()
        {
            return await bookRepository.FindAll();
        }

        public async Task<Book> FindById(long id)
        {
            return await bookRepository.FindById(id);
        }

        public async Task<long> Create(Book user)
        {
            return await bookRepository.Create(user);
        }
        
    }
}