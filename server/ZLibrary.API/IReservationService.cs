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
        Task<IList<Reservation>> FindBookReservations(long bookId);
        Task<Reservation> Order(Book book, User user);
        Task ApprovedReservation(Reservation reservation, Book book);
        Task RejectedReservation(Reservation reservation);
    }
}