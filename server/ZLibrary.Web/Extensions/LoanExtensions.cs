using System.Collections.Generic;
using System.Linq;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Extensions
{
    public static class LoanExtensions
    {
        public static LoanDTO ToLoanViewItem(this Loan loan)
        {
            return new LoanDTO()
            {
                Id = loan.Id,
                ReservationId = loan.Reservation.Id,
                ExpirationDate = loan.ExpirationDate,
                EndDate = loan.LoanEnd,
                StartDate = loan.LoanStart,
                CanBorrow = loan.CanBorrow,
                IsExpired = loan.IsExpired,
                IsReturned = loan.IsReturned

            };
        }

        public static IEnumerable<LoanDTO> ToLoanViewItems(this IEnumerable<Loan> loans)
        {
            return loans.Select(l => l.ToLoanViewItem()).ToArray();
        }
    }
}