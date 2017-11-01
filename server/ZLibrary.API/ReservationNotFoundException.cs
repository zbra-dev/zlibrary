using System;

namespace ZLibrary.API
{
    public class ReservationNotFoundException : Exception
    {
        public ReservationNotFoundException(string message) : base(message)
        {
            
        }
    }
}