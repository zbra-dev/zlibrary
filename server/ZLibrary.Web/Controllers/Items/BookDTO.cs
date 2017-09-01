using ZLibrary.Model;

namespace ZLibrary.Web
{
    public class BookDTO
    {
        public static BookDTO FromModel(Book book)
        {
            return new BookDTO() { PublisherId = book.Publisher.Id };
        }

        public long Id { get; set; }
        public long PublisherId { get; set; }
        public long[] AuthorIds { get; set; }
    }
}