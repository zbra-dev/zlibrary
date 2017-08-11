using System;

namespace ZLibrary.Model
{
    public class Reservation
    {
        public User User { get; private set; }
        public BookCopy BookCopy { get; private set; }
        public ReservationReason Reason { get; private set; }
        public Date StartDate { get; private set; }

        public Reservation(BookCopy bookCopy, User user)
        {

            if (bookCopy == null)
            {
                throw new ArgumentNullException($"The paramenter {nameof(bookCopy)} can not be null.");
            }
            if (user == null)
            {
                throw new ArgumentNullException($"The paramenter {nameof(user)} can not be null.");
            }

            User = user;
            BookCopy = bookCopy;
            StartDate = new Date(DateTime.Now);
            Reason = new ReservationReason();
        }
    }
}