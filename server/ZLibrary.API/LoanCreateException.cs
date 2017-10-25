using System;

namespace ZLibrary.API
{
    public class LoanCreateException : Exception
    {
         public LoanCreateException(string message) : base(message)
        {
            
        }
    }
}