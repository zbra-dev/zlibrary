using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using System.Collections.Generic;
using System.Linq;
using ZLibrary.Web.Controllers.Items;
using ZLibrary.Web.Validators;
using ZLibrary.Web.Extensions;
using System;

namespace ZLibrary.Web.Controllers
{
    [Route("api/[controller]")]
    public class LoansController : Controller
    {
        private readonly ILoanService loanService;
        private readonly IReservationService reservationService;

        public LoansController(ILoanService loanService, IReservationService reservationService)
        {
            this.loanService = loanService;
            this.reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var loans = await loanService.FindAll();
            return Ok(loans.ToLoanViewItems());
        }

        [HttpGet("{id:long}", Name = "FindLoan")]
        public async Task<IActionResult> FindById(long id)
        {
            var loan = await loanService.FindById(id);
            if (loan == null)
            {
                return NotFound($"Nenhuma loan encontrada com o ID: {id}.");
            }
            return Ok(loan.ToLoanViewItem());
        }

        [HttpGet("user/{id:long}", Name = "FindLoanByUser")]
        public async Task<IActionResult> FindByUserId(long userId)
        {
            var loans = await loanService.FindByUserId(userId);
            if (loans == null)
            {
                return NotFound($"Nenhuma loan encontrada com o userId: {userId}");
            }
            return Ok(loans.ToLoanViewItems());
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
                return NotFound($"Nenhuma loan encontrada com o ID: {id}");
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
                return NotFound($"Nenhuma loan encontrada com o ID: {id}");
            }
        }
    }
}