using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Converters
{
    public class ReservationCancelRequestConverter : AbstractToModelConverter<ReservationCancelRequest, ReservationCancelRequestDto>
    {
        protected override ReservationCancelRequest NullSafeConvertToModel(ReservationCancelRequestDto viewItem)
        {
            return new ReservationCancelRequest { ReservationId = viewItem.Id };
        }
    }
}
