using System;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Converters
{
    public class ReservationApproveRequestConverter : AbstractToModelConverter<ReservationApproveRequest, ReservationApproveRequestDto>
    {
        protected override ReservationApproveRequest NullSafeConvertToModel(ReservationApproveRequestDto viewItem)
        {
            return new ReservationApproveRequest { ReservationId = viewItem.Id };
        }
    }
}
