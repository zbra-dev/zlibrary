using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Converters
{
    public class ReservationHoldRequestConverter : AbstractToModelConverter<ReservationHoldRequest, ReservationHoldRequestDto>
    {
        protected override ReservationHoldRequest NullSafeConvertToModel(ReservationHoldRequestDto viewItem)
        {
            return new ReservationHoldRequest { ReservationId = viewItem.Id};
        }
    }
}
