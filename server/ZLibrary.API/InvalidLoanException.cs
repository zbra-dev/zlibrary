using System;
using System.Collections.Generic;
using System.Text;

namespace ZLibrary.API
{
    public class InvalidLoanException : Exception
    {
        public InvalidLoanException(string message) : base(message)
        {

        }
    }
}
