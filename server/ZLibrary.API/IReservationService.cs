using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.API
{
    public interface IReservationService
    {
        Task<IList<Reservation>> FindAll();
        Task<Reservation> FindById(long id);
        Task<IList<Reservation>> FindByUserId(long userId);
        Task<IList<Reservation>> FindByBookId(long bookId);
        Task<IList<Reservation>> FindBookReservations(long bookId);
        Task<Reservation> Order(Book book, User user);
        Task ApprovedReservation(Reservation reservation, Book book);
        Task AddToWaitingList(Reservation reservation, Book book);
        Task CancelReservation(Reservation reservation);
        Task ReturnReservation(Reservation reservation, Book book);
        Task OrderNext(long bookId);
        Task<IList<Reservation>> FindByStatus(ReservationStatus reservationStatus);
    }
}