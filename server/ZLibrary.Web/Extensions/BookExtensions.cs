using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;
using ZLibrary.Web.Validators;

namespace ZLibrary.Web.Extensions
{
    public static class BookExtensions
    {

        public static Book FromBookViewItem(this BookDTO bookDTO, ValidationResult validationResult)
        {
            var book = new Book()
            {
                Title = bookDTO.Title,
                Synopsis = bookDTO.Synopsis,
                PublicationYear = bookDTO.PublicationYear,
                Isbn = validationResult.GetResult<Isbn>(),
                Publisher = validationResult.GetResult<Publisher>(),
                Authors = validationResult.GetResult<List<BookAuthor>>(),
                NumberOfCopies = bookDTO.NumberOfCopies,
                CoverImageKey = bookDTO.CoverImageKey,
            };

            foreach (var bookAuthor in book.Authors)
            {
                bookAuthor.Book = book;
                bookAuthor.BookId = book.Id;
            }

            return book;
        }
        public static Book FromBookViewItem(this BookDTO bookDTO, Book book, ValidationResult validationResult)
        {
            book.Title = bookDTO.Title;
            book.Synopsis = bookDTO.Synopsis;
            book.PublicationYear = bookDTO.PublicationYear;
            book.Isbn = validationResult.GetResult<Isbn>();
            book.Publisher = validationResult.GetResult<Publisher>();
            book.Authors = validationResult.GetResult<List<BookAuthor>>();
            book.NumberOfCopies = bookDTO.NumberOfCopies;
            book.CoverImageKey = bookDTO.CoverImageKey;

            foreach (var bookAuthor in book.Authors)
            {
                bookAuthor.Book = book;
                bookAuthor.BookId = book.Id;
            }

            return book;
        }

        public async static Task<BookDTO> ToBookViewItem(this Book book, IReservationService reservationService, ILoanService loanService)
        {
            var bookDTO = new BookDTO()
            {
                Id = book.Id,
                Authors = book.Authors.Select(a => a.Author.ToAuthorViewItem()).ToArray(),
                Isbn = book.Isbn.Value,
                Publisher = book.Publisher.ToPublisherViewItem(),
                PublicationYear = book.PublicationYear,
                Title = book.Title,
                Synopsis = book.Synopsis,
                NumberOfCopies = book.NumberOfCopies,
                CoverImageKey = book.CoverImageKey,
            };

            var reservations = await reservationService.FindByBookId(book.Id);
            bookDTO.Reservations = await reservations.ToReservationViewItems(loanService);
            return bookDTO;
        }

        public async static Task<IEnumerable<BookDTO>> ToBookViewItems(this IEnumerable<Book> books, IReservationService reservationService, ILoanService loanService)
        {
             var tasks = books.Select(b => b.ToBookViewItem(reservationService, loanService));
            return await Task.WhenAll(tasks);
        }
    }
}