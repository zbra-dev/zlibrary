using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.Persistence
{
    public interface IReservationRepository
    {
        Task<IList<Reservation>> FindAll();
        Task<Reservation> FindById(long id);
        Task<IList<Reservation>> FindByUserId(long userId);
        Task<IList<Reservation>> FindByBookId(long BookId);
        Task<Reservation> Save(Reservation reservation);
        Task Update(Reservation reservation);
    }
}