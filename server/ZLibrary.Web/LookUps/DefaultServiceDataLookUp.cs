using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;

namespace ZLibrary.Web.LookUps
{
    public class DefaultServiceDataLookUp : IServiceDataLookUp
    {
        private readonly ILoanService loanService;
        private readonly IReservationService reservationService;

        public DefaultServiceDataLookUp(ILoanService loanService,  IReservationService reservationService)
        {
            this.loanService = loanService;
            this.reservationService = reservationService;
        }

        public async Task<Loan> LookUp(Reservation reservation)
        {
            return await loanService.FindByReservationId(reservation.Id);
        }

        public async Task<IEnumerable<Reservation>> LookUp(Book book)
        {
            return await reservationService.FindByBookId(book.Id);
        }
    }
}