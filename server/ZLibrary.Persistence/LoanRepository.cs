using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace ZLibrary.Persistence
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ZLibraryContext context;

        public LoanRepository(ZLibraryContext context)
        {
            this.context = context;
        }

        public async Task Create(Loan loan)
        {
            await context.Loans.AddAsync(loan);
            await context.SaveChangesAsync();
        }

        public async Task<IList<Loan>> FindAll()
        {
            return await context.Loans
            .Include(l => l.Reservation)
            .Include(l => l.Reservation.Reason)
            .Include(l => l.Reservation.User)
            .ToListAsync();
        }

        public async Task<Loan> FindById(long id)
        {
            return await context.Loans
            .Include(l => l.Reservation)
            .Include(l => l.Reservation.Reason)
            .Include(l => l.Reservation.User)
            .SingleOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IList<Loan>> FindByUserId(long userId)
        {
            return await context.Loans
            .Include(l => l.Reservation)
            .Include(l => l.Reservation.Reason)
            .Include(l => l.Reservation.User)
            .Where(l => l.Reservation.User.Id == userId)
            .ToListAsync();
        }

        public async Task<IList<Loan>> FindByBookId(long bookId)
        {
            return await context.Loans
            .Include(l => l.Reservation)
            .Include(l => l.Reservation.Reason)
            .Include(l => l.Reservation.User)
            .Where(l => l.Reservation.BookId == bookId)
            .ToListAsync();
        }

        public async Task<Loan> FindByReservationId(long reservationId)
        {
            return await context.Loans
                .Include(l => l.Reservation)
                .Include(l => l.Reservation.Reason)
                .Include(l => l.Reservation.User)
                .SingleOrDefaultAsync(l => l.Reservation.Id == reservationId);
        }

        public async Task<IList<Loan>> FindByReservationsIds(IList<long> reservationIds)
        {
            return await context.Loans
                .Include(l => l.Reservation)
                .Include(l => l.Reservation.Reason)
                .Include(l => l.Reservation.User)
                .Where(l => reservationIds.Contains(l.Reservation.Id))
                .ToListAsync();
        }

        public async Task<Loan> Update(Loan loan)
        {
            context.Loans.Update(loan);
            await context.SaveChangesAsync();
            return loan;
        }
    }
}