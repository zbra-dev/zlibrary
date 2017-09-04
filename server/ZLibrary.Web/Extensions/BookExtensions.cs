using System.Collections.Generic;
using System.Linq;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Extensions
{
    public static class BookExtensions
    {
        public static IEnumerable<BookDTO> ToBookViewItems(this IEnumerable<Book> books)
        {
            return books.Select(b => new BookDTO() 
            {
                Id = b.Id,
                AuthorIds = b.Authors.Select(a => a.AuthorId).ToArray(),
                Isbn = b.Isbn.Value,
                PublisherId = b.Publisher.Id,
                PublicationYear = b.PublicationYear,
                Title = b.Title,
                Synopsis = b.Synopsis
            }).ToArray();
        }
    }
}