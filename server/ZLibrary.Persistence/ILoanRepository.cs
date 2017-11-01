using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.Model;


namespace ZLibrary.Persistence
{
    public interface ILoanRepository
    {
        Task<IList<Loan>>  FindByUserId(long userId);
        Task<IList<Loan>>  FindByBookId(long bookId);
        Task<Loan> FindById(long id);
        Task<IList<Loan>> FindAll();
        Task<Loan> Update(Loan loan);
        Task Create(Loan loan);

    }
}