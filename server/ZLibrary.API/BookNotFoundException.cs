using System;
using System.Collections.Generic;
using System.Text;

namespace ZLibrary.API
{
    public class BookNotFoundException : Exception
    {
        public BookNotFoundException(string message) : base(message)
        {

        }
    }
}
