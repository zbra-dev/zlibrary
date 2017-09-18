using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.API.Exception;
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
            if (id <= 0)
            {
                return null;
            }

            return await bookRepository.FindById(id);
        }
        public async Task Delete(long id)
        {
            if (id <= 0)
            {
                return;
            }

            await bookRepository.Delete(id);
        }

        public async Task Save(Book book)
        {
            if (book.NumberOfCopies <= 0)
            {
                throw new BookSaveException("Número de cópias deve ser positivo maior que zero.");
            }

            await bookRepository.Save(book);
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