namespace ZLibrary.Model
{
    public class BookAuthor
    {
        public long BookId { get; set; }
        public Book Book { get; set; }
        public long AuthorId { get; set; }
        public Author Author { get; set; }
    }
}