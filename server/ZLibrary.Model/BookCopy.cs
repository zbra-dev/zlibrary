using System;

namespace ZLibrary.Model
{
    public class BookCopy
    {
        public Book Book { get; private set; }
        public BookExternalReference BookExternalReference { get; set; }

        public BookCopy(Book book, BookExternalReference bookExternalReference)
        {
            if (book == null)
            {
                throw new ArgumentNullException($"The paramenter {nameof(book)} can not be null.");
            }
            if (bookExternalReference == null)
            {
                throw new ArgumentNullException($"The paramenter {nameof(bookExternalReference)} can not be null.");
            }

            Book = book;
            BookExternalReference = bookExternalReference;
        }
    }
}