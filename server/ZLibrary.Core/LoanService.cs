using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using ZLibrary.Persistence;

namespace ZLibrary.Core
{
    public class LoanService : ILoanService
    {
        private ILoanRepository loanRepository;
        public LoanService(ILoanRepository loanRepository)
        {
            this.loanRepository = loanRepository;
        }
        
        public async Task Create(Loan loan)
        {
            if(loan == null)
            {
                throw new LoanCreateException("Loan não pode ser nulo");
            }
            await loanRepository.Create(loan);
        }

        public async Task<IList<Loan>> FindAll()
        {
            return await loanRepository.FindAll();
        }

        public async Task<Loan> FindById(long id)
        {
            return await loanRepository.FindById(id);
        }

        public async Task ExpireLoan(long id)
        {
            var loan = await FindById(id);

            if (loan == null)
            {
                throw new LoanNotFoundException();
            }

            loan.Status = LoanStatus.Expired;
            await loanRepository.Update(loan);
        }

        public async Task<IList<Loan>> FindByUserId(long userId)
        {
            return await loanRepository.FindByUserId(userId);
        }

        public async Task ReturnLoan(long id)
        {
            var loan = await FindById(id);

            if (loan == null)
            {
                throw new LoanNotFoundException();
            }

            loan.Status = LoanStatus.Returned;
            await loanRepository.Update(loan);
        }
    }
}