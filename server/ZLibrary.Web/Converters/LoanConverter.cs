using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Converters
{
    public class LoanConverter : AbstractFromModelConverter<Loan, LoanDto>
    {
        protected override LoanDto NullSafeConvertFromModel(Loan model)
        {
            return new LoanDto
            {
                Id = model.Id,
                ReservationId = model.Reservation.Id,
                ExpirationDate = model.ExpirationDate,
                EndDate = model.LoanEnd,
                StartDate = model.LoanStart,
                CanBorrow = model.CanBorrow,
                IsExpired = model.IsExpired,
                IsReturned = model.IsReturned
            };
        }
    }
}
