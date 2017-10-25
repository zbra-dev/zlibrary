using System;

namespace ZLibrary.Model
{
    public class Loan
    {
        public long Id { get; set; }
        public Reservation Reservation { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public LoanStatus Status { get; set; }

        public Loan(Reservation reservation)
        {
            if (reservation == null)
            {
                throw new ArgumentNullException($"The paramenter {nameof(reservation)} can not be null.");
            }
            Reservation = reservation;
            Status = LoanStatus.Borrowed;
            ExpirationDate = CalculateExpirationDate();
        }

        private Loan()
        {
        }

        private DateTime CalculateExpirationDate()
        {
            return DateTime.Now.AddMonths(3);
        }
    }
}