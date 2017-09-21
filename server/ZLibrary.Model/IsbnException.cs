using System;

namespace ZLibrary.API.Exception
{
    public class IsbnException : SystemException
    {
        public IsbnException(string message) : base(message)
        {
            
        }
    }
}