using System;

namespace ZLibrary.API
{
    public class BookSaveException : Exception
    {
        public BookSaveException(string message) : base(message)
        {
            
        }
    }
}