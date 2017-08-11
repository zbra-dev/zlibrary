using System.Collections.Generic;

namespace ZLibrary.Model
{
    public class Book
    {
        public string Title { get; set; }
        public long Id { get; set; }
        public Publisher Publisher { get; set; }
        public List<Author> Authors { get; set; }
        public Isbn Isbn { get; set; }
        public string Synopsis { get; set; }
        public int PublicationYear { get; set; }
        public CoverImage CoverImage { get; set; }
    }
}