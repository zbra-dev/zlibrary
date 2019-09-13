using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.Model;
using Microsoft.EntityFrameworkCore;
using Impress;

namespace ZLibrary.Persistence
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ZLibraryContext context;

        public readonly BookRepository bookRepository;

        public ReservationRepository(ZLibraryContext context, BookRepository bookRepository)
        {
            this.context = context;
            this.bookRepository = bookRepository;
        }
        public async Task<IList<Reservation>> FindAll()
        {
            var reservations = await context.Reservations
                 .Include(reservation => reservation.User)
                 .Include(reservation => reservation.Reason)
                 .Include(reservation => reservation.Book)
                 .ToListAsync();

            var loanedCopies = GetNumberOfLoanedCopies(reservations.Select(r => r.Book).ToSet());

            foreach(var reservation in reservations)
            {
                reservation.Book.NumberOfLoanedCopies = loanedCopies[reservation.BookId];
            }

            return reservations;
        }

        private IDictionary<long, int> GetNumberOfLoanedCopies(ISet<Book> books)
        {
            return bookRepository.GetLoanedCopies(books);
        }

        public async Task<IList<Reservation>> FindByBookId(long BookId)
        {
            var reservations = await context.Reservations.Where(r => r.BookId == BookId)
                .Include(reservation => reservation.User)
                .Include(reservation => reservation.Reason)
                .Include(reservation => reservation.Book)
                .ToListAsync();

            if (reservations != null && reservations.Any())
            {
                var bookSet = new HashSet<Book>();
                bookSet.Add(reservations.First().Book);

                var loanedCopies = GetNumberOfLoanedCopies(bookSet);

                foreach (var reservation in reservations)
                {
                    reservation.Book.NumberOfLoanedCopies = loanedCopies[reservation.BookId];
                }
            }

            return reservations;
        }

        public async Task<IList<Reservation>> FindByStatus(ReservationStatus reservationStatus)
        {
            var reservations = await context.Reservations.Where(r => r.Reason.Status == reservationStatus)
                .Include(reservation => reservation.User)
                .Include(reservation => reservation.Reason)
                .Include(reservation => reservation.Book)
                .ToListAsync();

            var loanedCopies = GetNumberOfLoanedCopies(reservations.Select(r => r.Book).ToSet());

            foreach (var reservation in reservations)
            {
                reservation.Book.NumberOfLoanedCopies = loanedCopies[reservation.BookId];
            }

            return reservations;
        }

        public async Task<IList<Reservation>> FindRequestedOrders()
        {
            var reservations = await context.Reservations.Where(r => r.Reason.Status == ReservationStatus.Requested
                                                        || r.Reason.Status == ReservationStatus.Waiting)
                .Include(reservation => reservation.User)
                .Include(reservation => reservation.Reason)
                .Include(reservation => reservation.Book)
                .ToListAsync();

            var loanedCopies = GetNumberOfLoanedCopies(reservations.Select(r => r.Book).ToSet());

            foreach (var reservation in reservations)
            {
                reservation.Book.NumberOfLoanedCopies = loanedCopies[reservation.BookId];
            }

            return reservations;
        }

        public async Task<Reservation> FindById(long id)
        {
            var reservation = await context.Reservations
                .Include(r => r.User)
                .Include(r => r.Reason)
                .Include(r => r.Book)
                .SingleOrDefaultAsync(r => r.Id == id);

            if (reservation != null)
            {
                var bookSet = new HashSet<Book>();
                bookSet.Add(reservation.Book);
                var loanedCopies = GetNumberOfLoanedCopies(bookSet);
                reservation.Book.NumberOfLoanedCopies = loanedCopies[reservation.BookId];
            }

            return reservation;
        }

        public async Task<IList<Reservation>> FindByUserId(long userId)
        {
            var reservations = await context.Reservations.Where(r => r.User.Id == userId)
                .Include(reservation => reservation.User)
                .Include(reservation => reservation.Reason)
                .Include(reservation => reservation.Book)
                .ToListAsync();

            var loanedCopies = GetNumberOfLoanedCopies(reservations.Select(r => r.Book).ToSet());

            foreach (var reservation in reservations)
            {
                reservation.Book.NumberOfLoanedCopies = loanedCopies[reservation.BookId];
            }

            return reservations;
        }

        public async Task Update(Reservation reservation)
        {
            context.Reservations.Update(reservation);
            await context.SaveChangesAsync();
        }

        public async Task<Reservation> Save(Reservation reservation)
        {
            await context.Reservations.AddAsync(reservation);
            await context.SaveChangesAsync();
            await context.Entry(reservation).ReloadAsync();
            return reservation;
        }
    }
}