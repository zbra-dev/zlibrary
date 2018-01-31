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
        private readonly IBookRepository bookRepository;
        private readonly IImageService imageService;
        private readonly IReservationService reservationService;

        public BookService(IBookRepository bookRepository, IImageService imageService, IReservationService reservationService)
        {
            this.bookRepository = bookRepository;
            this.imageService = imageService;
            this.reservationService = reservationService;
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

        public async Task<Book> FindByCoverImageKey(Guid key)
        {
            return await bookRepository.FindByCoverImageKey(key);
        }

        public async Task<bool> IsBookAvailable(Book book)
        {
            var reservations = await reservationService.FindBookReservations(book.Id);

            if (reservations == null)
            {
                return true;
            }

            return reservations.Count() < book.NumberOfCopies;
        }

        public async Task Delete(long id)
        {
            if (id <= 0)
            {
                return;
            }
            var reservations = await reservationService.FindBookReservations(id);
            if (reservations.Any())
            {
                throw new BookDeleteException("O Livro não pode ser deletado pois possui copias emprestadas.");
            }
            var book = await FindById(id);
            if (book != null)
            {
                imageService.DeleteFile(book.CoverImageKey);
                await bookRepository.Delete(id);
            }
        }

        public async Task Save(Book book)
        {
            if (book.NumberOfCopies <= 0)
            {
                throw new BookSaveException("Número de cópias deve ser positivo maior que zero.");
            }

            if (book.Id > 0)
            {
                var reservations = await reservationService.FindBookReservations(book.Id);
                if (book.NumberOfCopies < reservations.Count())
                {
                    throw new BookSaveException("Número de cópias não pode ser menor que a quantidade de livros emprestados.");
                }
            }

            await bookRepository.Save(book);
        }

        public async Task<IList<Book>> FindBy(BookSearchParameter parameters)
        {
            var bookSet = new HashSet<Book>();

            if (string.IsNullOrEmpty(parameters.Keyword))
            {
                bookSet.UnionWith(await FindAll());
            }
            else
            {
                var booksByTitle = await bookRepository.FindByTitleOrSynopsis(parameters.Keyword);
                bookSet.UnionWith(booksByTitle);

                var booksByIsbn = await bookRepository.FindByIsbn(parameters.Keyword);
                bookSet.UnionWith(booksByIsbn);

                var booksByPublisher = await bookRepository.FindByPublisher(parameters.Keyword);
                bookSet.UnionWith(booksByPublisher);

                var booksByAuthor = await bookRepository.FindByAuthor(parameters.Keyword);
                bookSet.UnionWith(booksByAuthor);
            }
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