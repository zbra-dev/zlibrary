using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

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

        public async static Task<ReservationResultDTO> ToReservationViewItem(this Reservation reservation, ILoanService loanService)
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

            var loan = await loanService.FindByReservationId(reservation.Id);

            if (loan != null)
            {
                reservationDTO.IsLoanExpired = loan.IsExpired;
                reservationDTO.CanBorrow = loan.CanBorrow;
            }
            return reservationDTO;
        }

        public async static Task<IEnumerable<ReservationResultDTO>> ToReservationViewItems(this IEnumerable<Reservation> reservations, ILoanService loanService)
        {
            var tasks =  reservations.Select(r => r.ToReservationViewItem(loanService));
            return await Task.WhenAll(tasks);
        }
    }
}