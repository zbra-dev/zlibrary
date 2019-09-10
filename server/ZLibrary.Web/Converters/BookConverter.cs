using System.Linq;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Converters
{
    public class BookConverter : AbstractConverter<Book, BookDto>
    {
        private readonly PublisherConverter publisherConverter;
        private readonly AuthorConverter authorConverter;

        public BookConverter(PublisherConverter publisherConverter, AuthorConverter authorConverter)
        {
            this.publisherConverter = publisherConverter;
            this.authorConverter = authorConverter;
        }

        protected override BookDto NullSafeConvertFromModel(Book model)
        {
            return new BookDto
            {
                Id = model.Id,
                Title = model.Title,
                Synopsis = model.Synopsis,
                PublicationYear = model.PublicationYear,
                Isbn = model.Isbn.ToString(),
                Publisher = publisherConverter.ConvertFromModel(model.Publisher),
                Authors = model.Authors.Select(ba => authorConverter.ConvertFromModel(ba.Author)).ToArray(),
                NumberOfCopies = model.NumberOfCopies,
                NumberOfLoanedCopies = model.NumberOfLoanedCopies,
                NumberOfAvailableCopies = model.CalculateNumberOfAvailableCopies(),
                CoverImageKey = model.CoverImageKey,
                Edition =  model.Edition
            };
        }

        protected override Book NullSafeConvertToModel(BookDto viewItem)
        {
            var model = new Book
            {
                Id = viewItem.Id,
                Title = viewItem.Title,
                Synopsis = viewItem.Synopsis,
                PublicationYear = viewItem.PublicationYear,
                Isbn = Isbn.FromString(viewItem.Isbn),
                Publisher = publisherConverter.ConvertToModel(viewItem.Publisher),
                NumberOfCopies = viewItem.NumberOfCopies,
                CoverImageKey = viewItem.CoverImageKey,
                Edition = viewItem.Edition
            };

            foreach (var author in viewItem.Authors)
            {
                var bookAuthor = new BookAuthor();
                bookAuthor.Book = model;
                bookAuthor.BookId = model.Id;
                bookAuthor.Author = authorConverter.ConvertToModel(author);
                bookAuthor.AuthorId = author.Id.HasValue ? author.Id.Value : 0;

                model.Authors.Add(bookAuthor);
            }

            return model;
        }
    }
}
