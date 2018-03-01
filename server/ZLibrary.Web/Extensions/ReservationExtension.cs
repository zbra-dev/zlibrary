using System.Collections.Generic;
using System.Linq;
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
               Book = new BookDTO() { Id = reservation.BookId },
               UserId = reservation.User.Id,
               Description = reservation.Reason.Description,
               StatusId = (long)reservation.Reason.Status,
               StartDate = reservation.StartDate
            };
        }

        public static IEnumerable<ReservationResultDTO> ToReservationViewItems(this IEnumerable<Reservation> reservations)
        {
            return reservations.Select(r => r.ToReservationViewItem()).ToArray();
        }
    }
}