using System;

namespace ZLibrary.API
{
    public class ReservationApprovedException : Exception
    {
        public ReservationApprovedException(string message) : base(message)
        {
        }
    }
}