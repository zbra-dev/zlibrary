using System;

namespace ZLibrary.Model
{

    public class Reservation
    {

        public User User { get; private set; }
        public BookCopy BookCopy { get; private set; }
        public ReservationReason Reason { get; set; }
        public Date StartingDate { get; private set; }

        public Reservation(BookCopy bookCopy, User user){

            User = user;
            BookCopy = bookCopy;
            StartingDate = new Date(DateTime.Now);
            Reason.Status = ReservationStatus.Requested;

        }


    }

}