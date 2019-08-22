using System;
using System.Collections.Generic;
using System.Text;

namespace ZLibrary.API
{
    public class AuthorSaveException : Exception
    {
        public AuthorSaveException(string message) : base(message) { }
    }
}
