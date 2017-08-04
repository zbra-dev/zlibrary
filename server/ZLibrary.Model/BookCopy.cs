namespace ZLibrary.Model
{

    public class BookCopy
    {

        public Book Book { get; private set; }
        public BookExternalReference BookExternalReference { get; set; }

        public BookCopy(Book book, BookExternalReference bookExternalReference)
        {
            Book = book;
            BookExternalReference = bookExternalReference;
        }

    }

}