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
        public async Task Delete(long id)
        {
            await bookRepository.Delete(id);
        }

        public async Task<long> Save(Book book)
        {
            return await bookRepository.Create(book);
        }

        public async Task<IList<Book>> FindBy(BookSearchParameter parameters) 
        {
            var bookSet = new HashSet<Book>();

            var booksByTitle = await bookRepository.FindByTitleOrSynopsis(parameters.Keyword);
            bookSet.UnionWith(booksByTitle);

            var booksByIsbn = await bookRepository.FindByIsbn(parameters.Keyword);
            bookSet.UnionWith(booksByIsbn);

            var booksByPublisher = await bookRepository.FindByPublisher(parameters.Keyword);
            bookSet.UnionWith(booksByPublisher);

            var booksByAuthor = await bookRepository.FindByAuthor(parameters.Keyword);
            bookSet.UnionWith(booksByAuthor);

            Func<Book, object> orderBySelector;

            if (parameters.OrderBy == SearchOrderBy.Created)
            {
                orderBySelector = b => b.Created;
            }
            else 
            {
                orderBySelector = b => b.Title;
            }

            return bookSet.OrderBy(orderBySelector).ToArray();
        }
    }
}