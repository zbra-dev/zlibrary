using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;
using ZLibrary.Web.LookUps;
using ZLibrary.Web.Validators;

namespace ZLibrary.Web.Extensions
{
    public static class BookExtensions
    {
        public static Book FromBookViewItem(this BookDTO bookDTO, IValidationResultDataLookUp dataLookUp)
        {
            var book = new Book()
            {
                Id = bookDTO.Id,
                Title = bookDTO.Title,
                Synopsis = bookDTO.Synopsis,
                PublicationYear = bookDTO.PublicationYear,
                Isbn = dataLookUp.LookUp<Isbn>(),
                Publisher = dataLookUp.LookUp<Publisher>(),
                Authors = dataLookUp.LookUp<List<BookAuthor>>(),
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
        
        public async static Task<BookDTO> ToBookViewItem(this Book book, IServiceDataLookUp serviceDataLookUp)
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

            var reservations = await serviceDataLookUp.LookUp(book);
            bookDTO.Reservations = await reservations.ToReservationViewItems(serviceDataLookUp);
            return bookDTO;
        }

        public async static Task<IEnumerable<BookDTO>> ToBookViewItems(this IEnumerable<Book> books, IServiceDataLookUp serviceDataLookUp)
        {
            return await Task.WhenAll(books.Select(b => b.ToBookViewItem(serviceDataLookUp)));
        }
    }
}