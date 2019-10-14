using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Converters
{
    public class ReservationReturnRequestConverter : AbstractToModelConverter<ReservationReturnRequest, ReservationReturnRequestDto>
    {
        protected override ReservationReturnRequest NullSafeConvertToModel(ReservationReturnRequestDto viewItem)
        {
            return new ReservationReturnRequest { ReservationId = viewItem.Id };
        }
    }
}
