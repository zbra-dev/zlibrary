using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.Web.LookUps
{
    public interface IServiceDataLookUp
    {
         Task<Loan> LookUp(Reservation reservation);
         Task<IEnumerable<Reservation>> LookUp(Book book);
    }
}