using System;

namespace ZLibrary.API.Exception
{
    public class BookSaveException : SystemException
    {
        public BookSaveException(string message) : base(message)
        {
            
        }
    }
}