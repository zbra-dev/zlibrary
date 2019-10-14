using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Converters
{
    public class RichReservationConverter : AbstractFromModelConverter<RichReservation, ReservationResultDto>
    {

        private readonly LoanConverter loanConverter;

        public RichReservationConverter(LoanConverter loanConverter)
        {
            this.loanConverter = loanConverter;
        }

        protected override ReservationResultDto NullSafeConvertFromModel(RichReservation model)
        {
            return new ReservationResultDto()
            {
                Id = model.Reservation.Id,
                BookId = model.Reservation.BookId,
                UserId = model.Reservation.User.Id,
                Description = model.Reservation.Reason.Description,
                StatusId = (long)model.Reservation.Reason.Status,
                StartDate = model.Reservation.StartDate,
                Loan = loanConverter.ConvertFromModel(model.Loan),
            };
        }
    }
}
