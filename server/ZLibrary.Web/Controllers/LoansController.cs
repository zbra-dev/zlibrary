using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZLibrary.API;
using System.Linq;
using ZLibrary.Web.Converters;

namespace ZLibrary.Web.Controllers
{
    [Route("api/[controller]")]
    public class LoansController : Controller
    {
        private readonly ILoanService loanService;
        private readonly IReservationService reservationService;
        private readonly LoanConverter loanConverter;

        public LoansController(ILoanService loanService, IReservationService reservationService, LoanConverter loanConverter)
        {
            this.loanService = loanService;
            this.reservationService = reservationService;
            this.loanConverter = loanConverter;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var loans = await loanService.FindAll();
            return Ok(loans.Select(l => loanConverter.ConvertFromModel(l)));
        }

        [HttpGet("{id:long}", Name = "FindLoan")]
        public async Task<IActionResult> FindById(long id)
        {
            var loan = await loanService.FindById(id);
            if (loan == null)
            {
                return NotFound($"Nenhum empréstimo encontrado com o ID: {id}.");
            }
            return Ok(loanConverter.ConvertFromModel(loan));
        }

        [HttpGet("user/{userId:long}", Name = "FindLoanByUser")]
        public async Task<IActionResult> FindByUserId(long userId)
        {
            var loans = await loanService.FindByUserId(userId);
            if (loans == null)
            {
                return NotFound($"Nenhum empréstimo encontrado com o userId: {userId}");
            }
            return Ok(loans.Select(l => loanConverter.ConvertFromModel(l)));
        }

        [HttpPost("expired/{id:long}")]
        public async Task<IActionResult> UpdateLoanStatusToExpired(long id)
        {
            try
            {
                await loanService.ExpireLoan(id);
                return Ok();
            }
            catch (LoanNotFoundException)
            {
                return NotFound($"Nenhum empréstimo encontrado com o ID: {id}");
            }
        }

        [HttpPost("returned/{id:long}")]
        public async Task<IActionResult> UpdateLoanStatusToReturned(long id)
        {
            try
            {
                var loan = await loanService.ReturnLoan(id);
                await reservationService.OrderNext(loan.Reservation.BookId);
                return Ok();
            }
            catch (LoanNotFoundException)
            {
                return NotFound($"Nenhum empréstimo encontrado com o ID: {id}");
            }
        }
    }
}