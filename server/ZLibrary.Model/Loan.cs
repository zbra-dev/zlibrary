using System;

namespace ZLibrary.Model
{

    public class Loan
    {

        public Reservation Reservation { get; private set; }
        public Date ExpirationDate { get; private set; }
        public LoanStatus Status { get; set; }

        public Loan(Reservation reservation)
        {

            Reservation = reservation;
            Status = LoanStatus.Borrowed;

            this.CalculateExpirationDate();

        }

        private void CalculateExpirationDate()
        {

            DateTime now = DateTime.Now;

            ExpirationDate = new Date(now.Add(new TimeSpan(90, 0, 0, 0)));

        }

    }


}