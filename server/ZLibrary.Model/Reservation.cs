using System;

namespace ZLibrary.Model
{
    public class Reservation
    {
        public long Id { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
        public long BookId { get; set; }
        public ReservationReason Reason { get; set; }
        public DateTime StartDate { get; set; }

        public bool IsRequested => Reason.Status == ReservationStatus.Requested;
        public bool IsApproved => Reason.Status == ReservationStatus.Approved;
        public bool IsCanceled =>  Reason.Status == ReservationStatus.Canceled;
        public bool IsWaiting => Reason.Status == ReservationStatus.Waiting;
        public bool IsReturned => Reason.Status == ReservationStatus.Returned;

        public Reservation(long bookId, User user)
        {
            if (bookId <= 0)
            {
                throw new ArgumentNullException($"O parâmetro {nameof(bookId)} deve ser maior que zero.");
            }
            if (user == null)
            {
                throw new ArgumentNullException($"O parametro {nameof(user)} não pode ser nulo.");
            }

            User = user;
            BookId = bookId;
            StartDate = DateTime.Now;
            Reason = new ReservationReason();
        }

        public Reservation()
        {
        }

       
    }
}