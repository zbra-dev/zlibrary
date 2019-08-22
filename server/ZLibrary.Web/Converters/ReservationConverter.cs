using ZLibrary.API;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Converters
{
    public class ReservationConverter : AbstractFromModelConverter<Reservation, ReservationResultDto>
    {
        protected override ReservationResultDto NullSafeConvertFromModel(Reservation model)
        {
            return new ReservationResultDto()
            {
                Id = model.Id,
                BookId = model.BookId,
                UserId = model.User.Id,
                Description = model.Reason.Description,
                StatusId = (long)model.Reason.Status,
                StartDate = model.StartDate
            };
        }
    }
}
