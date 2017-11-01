using System;

namespace ZLibrary.API
{
    public class BookDeleteException : Exception
    {
        public BookDeleteException(string message) : base(message)
        {
        }
    }
}