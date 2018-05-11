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

        public async Task<IList<Reservation>> FindByBookId(long bookId)
        {
            return await reservationRepository.FindByBookId(bookId);
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
            if (reservation.IsApproved || reservation.IsRejected)
            {
                throw new ReservationApprovedException($"O Status da reserversa precisa ser Solicitado ou Aguardando.");
            }

            if (book == null)
            {
                throw new InvalidOperationException("Livro não pode ser nulo.");
            }

            var loans = await loanService.FindByBookId(reservation.BookId);
            var loanBorrowedByUser = loans.SingleOrDefault(l => l.Reservation.User.Id == reservation.User.Id && l.Status == LoanStatus.Borrowed);
            if (loanBorrowedByUser != null)
            {
                await CreateLoan(reservation);
                await loanService.ReturnLoan(loanBorrowedByUser.Id);
            }
            else if (book.CanApproveLoan(loans))
            {
                await CreateLoan(reservation);
            }
            else
            {
                reservation.Reason.Status = ReservationStatus.Waiting;
                await reservationRepository.Update(reservation);
            }
        }

        public async Task RejectedReservation(Reservation reservation)
        {
            if (!reservation.IsRequested)
            {
                throw new ReservationApprovedException($"O Status da reserversa precsisa ser Solicitado.");
            }
            reservation.Reason.Status = ReservationStatus.Rejected;
            await reservationRepository.Update(reservation);
        }

        public async Task<Reservation> Order(Book book, User user)
        {
            return await reservationRepository.Save(new Reservation(book.Id, user));
        }

        public async Task OrderNext(long bookId)
        {
            var reservations = await reservationRepository.FindByBookId(bookId);
            var firstReservationWainting = reservations.OrderBy(r => r.StartDate).FirstOrDefault(r => r.Reason.Status == ReservationStatus.Waiting);

            if (firstReservationWainting != null)
            {
                await CreateLoan(firstReservationWainting);
            }
        }

        private async Task CreateLoan(Reservation reservation)
        {
            reservation.Reason.Status = ReservationStatus.Approved;
            await reservationRepository.Update(reservation);
            var loan = new Loan(reservation);
            await loanService.Create(loan);
        }
    }
}