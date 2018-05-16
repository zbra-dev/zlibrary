using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;
using ZLibrary.Web.LookUps;

namespace ZLibrary.Web.Extensions
{
    public static class ReservationExtensions
    {
        public static ReservationResultDTO ToReservationViewItem(this Reservation reservation)
        {
            return new ReservationResultDTO()
            {
                Id = reservation.Id,
                BookId = reservation.BookId,
                UserId = reservation.User.Id,
                Description = reservation.Reason.Description,
                StatusId = (long)reservation.Reason.Status,
                StartDate = reservation.StartDate
            };
        }

        public async static Task<ReservationResultDTO> ToReservationViewItem(this Reservation reservation, IServiceDataLookUp serviceDataLookUp)
        {
            var reservationDTO = new ReservationResultDTO()
            {
                Id = reservation.Id,
                BookId = reservation.BookId,
                UserId = reservation.User.Id,
                Description = reservation.Reason.Description,
                StatusId = (long)reservation.Reason.Status,
                StartDate = reservation.StartDate
            };

            var loan = await serviceDataLookUp.LookUp(reservation);
            if (loan != null)
            {
                reservationDTO.Loan = loan.ToLoanViewItem();
            }

            return reservationDTO;
        }

        public async static Task<IEnumerable<ReservationResultDTO>> ToReservationViewItems(this IEnumerable<Reservation> reservations, IServiceDataLookUp serviceDataLookUp)
        {
            return await Task.WhenAll(reservations.Select(r => r.ToReservationViewItem(serviceDataLookUp)));
        }
    }
}