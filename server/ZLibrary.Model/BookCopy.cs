using System;

namespace ZLibrary.Model
{
    public class BookCopy
    {
        public Book Book { get; private set; }
        public BookExternalReference BookExternalReference { get; set; }
        
        public BookCopy(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException($"The paramenter {nameof(book)} can not be null.");
            }
            Book = book;
            BookExternalReference = BookExternalReference.Empty;
        }
    }
}