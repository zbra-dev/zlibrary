using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using ZLibrary.Persistence;

namespace ZLibrary.Core
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository reservationRepository;
        private readonly ILoanService loanService;

        public ReservationService(IReservationRepository reservationRepository, ILoanService loanService)
        {
            this.reservationRepository = reservationRepository;
            this.loanService = loanService;
        }

        public async Task<IList<Reservation>> FindAll()
        {
            return await reservationRepository.FindAll();
        }

        public async Task<Reservation> FindById(long id)
        {
            if (id <= 0)
            {
                return null;
            }
            return await reservationRepository.FindById(id);
        }

        public async Task<IList<Reservation>> FindBookReservations(long bookId)
        {
            if (bookId <= 0)
            {
                return null;
            }
            var loans = await loanService.FindByBookId(bookId);
            var loansBorrowed = loans.Where(l => l.Status != LoanStatus.Returned).ToList();
            var reservations = await reservationRepository.FindByBookId(bookId);
            var reservationApprovedIds = reservations.Where(r => r.Reason.Status == ReservationStatus.Approved).Select(r => r.Id).ToList();

            return loansBorrowed.Where(l => reservationApprovedIds.Contains(l.Reservation.Id)).Select(l => l.Reservation).ToList();
        }

        public async Task<IList<Reservation>> FindByUserId(long userId)
        {
            if (userId <= 0)
            {
                return null;
            }
            return await reservationRepository.FindByUserId(userId);
        }

        public async Task ApprovedReservation(Reservation reservation, Book book)
        {
            if (reservation.IsApproved() || reservation.IsRejected())
            {
                throw new ReservationApprovedException($"O Status da reserversa precisa ser '{ReservationStatus.Requested}' ou '{ReservationStatus.Waiting}'.");
            }

            if (book == null)
            {
                throw new InvalidOperationException("Livro não pode ser nulo.");
            }

            var loans = await loanService.FindByBookId(reservation.BookId);
            if (book.CanApproveLoan(loans))
            {
                reservation.Reason.Status = ReservationStatus.Approved;
                await reservationRepository.Update(reservation);
                var loan = new Loan(reservation);
                await loanService.Create(loan);
            }
            else
            {
                reservation.Reason.Status = ReservationStatus.Waiting;
                await reservationRepository.Update(reservation);
            }
        }

        public async Task RejectedReservation(Reservation reservation)
        {
            if (!reservation.IsRequested())
            {
                throw new ReservationApprovedException($"O Status da reserversa precsisa ser '{ReservationStatus.Requested}'.");
            }
            reservation.Reason.Status = ReservationStatus.Rejected;
            await reservationRepository.Update(reservation);
        }

        public async Task<Reservation> Order(Book book, User user)
        {
            return await reservationRepository.Save(new Reservation(book.Id, user));
        }
    }
}