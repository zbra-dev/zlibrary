
namespace ZLibrary.Model
{
    public class RichReservation
    {
        public Reservation Reservation { get; set; }
        public Loan Loan { get; set; }

        public RichReservation(Reservation reservation, Loan loan)
        {
            this.Reservation = reservation;
            this.Loan = loan;
        }
    }
}
