using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.API
{
    public interface IReservationFacade
    {
        Task<IList<RichReservation>> FindAll();
        Task<RichReservation> FindById(long id);
        Task<IList<RichReservation>> FindByUserId(long userId);
        Task<IList<RichReservation>> FindRequestedOrders();
        Task<RichReservation> CreateOrder(long bookId, long userId);
        Task ApproveReservation(ReservationApproveRequest reservationApproveRequest);
        Task QueueReservation(ReservationHoldRequest reservationHoldRequest);
        Task ReturnReservation(ReservationReturnRequest reservationReturnRequest);
        Task CancelReservation(ReservationCancelRequest reservationCancelRequest);
        Task<IList<RichReservation>> FindByStatus(ReservationStatus reservationStatus);

    }
}
