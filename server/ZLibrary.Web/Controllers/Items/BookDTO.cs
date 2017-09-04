using System.Runtime.Serialization;
using ZLibrary.Model;

namespace ZLibrary.Web.Controllers.Items
{
    public class BookDTO
    {
        public static BookDTO FromModel(Book book)
        {
            return new BookDTO() { PublisherId = book.Publisher.Id };
        }

        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "publisherId")]
        public long PublisherId { get; set; }
        
        [DataMember(Name = "authorIds")]
        public long[] AuthorIds { get; set; }
        
        [DataMember(Name = "isbn")]
        public string Isbn { get; set; }
        
        [DataMember(Name = "synopsis")]
        public string Synopsis { get; set; }
        
        [DataMember(Name = "publicationYear")]
        public int PublicationYear { get; set; }
    }
}