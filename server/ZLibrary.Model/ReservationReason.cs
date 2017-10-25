namespace ZLibrary.Model
{
    public class ReservationReason
    {
        public long Id { get; set; }
        public ReservationStatus Status { get; set; }
        public string Description { get; set; }

        public ReservationReason()
        {
            Status = ReservationStatus.Requested;
        }
    }
}