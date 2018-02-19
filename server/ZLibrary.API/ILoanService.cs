using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.API
{
    public interface ILoanService
    {
        Task<IList<Loan>> FindAll();
        Task<IList<Loan>> FindByUserId(long userId);
        Task<IList<Loan>> FindByBookId(long bookId);
        Task<Loan> FindById(long id);
        Task<Loan> FindByReservationId(long reservationId);
        Task ExpireLoan(long id);
        Task<Loan> ReturnLoan(long id);
        Task Create(Loan loan);
    }
}
