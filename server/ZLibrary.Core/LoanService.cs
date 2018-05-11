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
        private readonly ILoanRepository loanRepository;

        public LoanService(ILoanRepository loanRepository)
        {
            this.loanRepository = loanRepository;
        }

        public async Task Create(Loan loan)
        {
            if (loan == null)
            {
                throw new LoanCreateException("Empréstimo não pode ser nulo");
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

        public async Task<Loan> FindByReservationId(long reservationId)
        {
            if (reservationId <= 0)
            {
                return null;
            }
            return await loanRepository.FindByReservationId(reservationId);
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

        public async Task<IList<Loan>> FindByBookId(long bookId)
        {
            return await loanRepository.FindByBookId(bookId);
        }

        public async Task<Loan> ReturnLoan(long id)
        {
            var loan = await FindById(id);

            if (loan == null)
            {
                throw new LoanNotFoundException();
            }

            loan.Status = LoanStatus.Returned;
            loan.LoanEnd = DateTime.Now;
            return await loanRepository.Update(loan);
        }
    }
}