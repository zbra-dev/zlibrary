using System.Collections.Generic;
using System.Linq;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Extensions
{
    public static class BookExtensions
    {

        public static BookDTO ToBookViewItem(this Book book)
        {
            return new BookDTO() 
            {
                Id = book.Id,
                AuthorIds =book.Authors.Select(a => a.AuthorId).ToArray(),
                Isbn = book.Isbn.Value,
                PublisherId = book.Publisher.Id,
                PublicationYear = book.PublicationYear,
                Title =book.Title,
                Synopsis = book.Synopsis,
                NumberOfCopies = book.NumberOfCopies
            };
        }

        public static IEnumerable<BookDTO> ToBookViewItems(this IEnumerable<Book> books)
        {
            return books.Select(b => b.ToBookViewItem()).ToArray();
        }
    }
}