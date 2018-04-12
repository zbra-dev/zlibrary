using System;

namespace ZLibrary.Model
{
    public class Loan
    {
        public long Id { get; set; }
        public Reservation Reservation { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public LoanStatus Status { get; set; }

        public bool IsReturned => Status == LoanStatus.Returned;
        public bool IsExpired => DateTime.Now > ExpirationDate;
        public bool CanBorrow => DateTime.Now >= ExpirationDate.AddMonths(-1);

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