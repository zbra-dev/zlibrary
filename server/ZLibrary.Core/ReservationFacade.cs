using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using ZLibrary.Persistence;

namespace ZLibrary.Core
{
    public class ReservationFacade : IReservationFacade
    {
        private readonly IReservationService reservationService;
        private readonly IUserRepository userRepository;
        private readonly IBookRepository bookRepository;
        private readonly ILoanRepository loanRepository;
        private readonly IReservationRepository reservationRepository;

        public ReservationFacade(IReservationService reservationService, IUserRepository userRepository,
            IBookRepository bookRepository, ILoanRepository loanRepository, IReservationRepository reservationRepository)
        {
            this.reservationService = reservationService;
            this.userRepository = userRepository;
            this.bookRepository = bookRepository;
            this.loanRepository = loanRepository;
            this.reservationRepository = reservationRepository;
        }

        public async Task<IList<RichReservation>> FindAll()
        {
            var reservations = await reservationRepository.FindAll();

            return await EnrichReservations(reservations);
        }

        private async Task<IList<RichReservation>> EnrichReservations(IList<Reservation> reservations)
        {
            var richReservations = new List<RichReservation>();
            var loans = await loanRepository.FindByReservationsIds(reservations.Select(r => r.Id).ToList());

            foreach (var reservation in reservations)
            {
                var loan = loans.SingleOrDefault(l => l.Reservation.Equals(reservation));
                richReservations.Add(EnrichReservation(reservation, loan).Result);
            }

            return richReservations;
        }

        public async Task<RichReservation> FindById(long id)
        {
            var reservation = await reservationRepository.FindById(id);

            if (reservation == null)
            {
                throw new ReservationNotFoundException($"Nenhuma reserva foi encontrada com o Id: {id}.");
            }

            var loan = await loanRepository.FindByReservationId(reservation.Id);

            return await EnrichReservation(reservation, loan);
        }

        private async Task<RichReservation> EnrichReservation(Reservation reservation, Loan loan)
        {
            return await Task.FromResult(new RichReservation(reservation, loan));
        }

        public async Task<IList<RichReservation>> FindByUserId(long userId)
        {
            var reservations = await reservationRepository.FindByUserId(userId);

            return await EnrichReservations(reservations);
        }

        public async Task<IList<RichReservation>> FindRequestedOrders()
        {
            var reservations = await reservationRepository.FindRequestedOrders();

            return await EnrichReservations(reservations);
        }

        public async Task<IList<RichReservation>> FindByStatus(ReservationStatus reservationStatus)
        {
            var reservations = await reservationRepository.FindByStatus(reservationStatus);

            return await EnrichReservations(reservations);
        }

        public async Task<RichReservation> CreateOrder(long bookId, long userId)
        {
            var user = await userRepository.FindById(userId);

            var savedReservation = await reservationRepository.Save(new Reservation(bookId, user));

            var loan = await loanRepository.FindByReservationId(savedReservation.Id);

            return await EnrichReservation(savedReservation, loan);
        }

        public async Task ApproveReservation(ReservationApproveRequest reservationApproveRequest)
        {
            var reservation = await reservationRepository.FindById(reservationApproveRequest.ReservationId);

            if (reservation == null)
            {
                throw new ReservationNotFoundException($"Nenhuma reserva foi encontrada com o Id: {reservationApproveRequest.ReservationId}.");
            }

            var book = await bookRepository.FindById(reservation.BookId);


            if (book == null)
            {
                throw new BookNotFoundException($"Nenhum livro foi encontrado com o Id da reserva: {reservationApproveRequest.ReservationId}.");
            }

            await reservationService.ApproveReservation(reservation, book);
        }

        public async Task QueueReservation(ReservationHoldRequest reservationHoldRequest)
        {
            var reservation = await reservationRepository.FindById(reservationHoldRequest.ReservationId);

            if (reservation == null)
            {
                throw new ReservationNotFoundException($"Nenhuma reserva foi encontrada com o Id: {reservationHoldRequest.ReservationId}.");
            }

            await reservationService.QueueReservation(reservation);
        }

        public async Task ReturnReservation(ReservationReturnRequest reservationReturnRequest)
        {
            var reservation = await reservationRepository.FindById(reservationReturnRequest.ReservationId);

            if (reservation == null)
            {
                throw new ReservationNotFoundException($"Nenhuma reserva foi encontrada com o Id: {reservationReturnRequest.ReservationId}.");
            }

            var book = await bookRepository.FindById(reservation.BookId);

            if (book == null)
            {
                throw new BookNotFoundException($"Nenhum livro foi encontrado com o Id da reserva: {reservationReturnRequest.ReservationId}.");
            }

            await reservationService.ReturnReservation(reservation, book);
        }

        public async Task CancelReservation(ReservationCancelRequest reservationCancelRequest)
        {
            var reservation = await reservationRepository.FindById(reservationCancelRequest.ReservationId);
            if (reservation == null)
            {
                throw new ReservationNotFoundException($"Nenhuma reserva foi encontrada com o Id: {reservationCancelRequest.ReservationId}.");
            }

            await reservationService.CancelReservation(reservation);
        }
    }
}
