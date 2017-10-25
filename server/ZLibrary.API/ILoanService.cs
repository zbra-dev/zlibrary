using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;

namespace ZLibrary.API
{
    public interface ILoanService
    {
        Task<IList<Loan>> FindAll();
        Task<IList<Loan>> FindByUserId(long userId);
        Task<Loan> FindById(long id);
        Task ExpireLoan(long id);
        Task ReturnLoan(long id);
        Task Create(Loan loan);
    }
}