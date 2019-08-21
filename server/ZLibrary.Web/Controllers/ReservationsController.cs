using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using System.Collections.Generic;
using System.Linq;
using ZLibrary.Web.Controllers.Items;
using System;
using ZLibrary.Web.LookUps;
using ZLibrary.Web.Converters;

namespace ZLibrary.Web
{
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {
        private readonly IReservationService reservationService;
        private readonly IUserService userService;
        private readonly IBookService bookService;
        private readonly ILoanService loanService;
        private readonly IServiceDataLookUp serviceDataLookUp;
        private readonly UserConverter userConverter;
        private readonly ReservationConverter reservationConverter;
        private readonly BookConverter bookConverter;
        private readonly LoanConverter loanConverter;

        public ReservationsController(IReservationService reservationService, IUserService userService, 
            IBookService bookService, ILoanService loanService, UserConverter userConverter, ReservationConverter reservationConverter,
            BookConverter bookConverter, LoanConverter loanConverter)
        {
            this.reservationService = reservationService;
            this.userService = userService;
            this.bookService = bookService;
            this.loanService = loanService;
            this.serviceDataLookUp = new DefaultServiceDataLookUp(loanService, reservationService);
            this.userConverter = userConverter;
            this.reservationConverter = reservationConverter;
            this.bookConverter = bookConverter;
            this.loanConverter = loanConverter;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var reservations = await reservationService.FindAll();
            return Ok(reservations.Select(r => reservationConverter.ConvertFromModel(r)));
        }

        [HttpGet("{id:long}", Name = "FindReservation")]
        public async Task<IActionResult> FindById(long id)
        {
            var reservation = await reservationService.FindById(id);
            if (reservation == null)
            {
                return NotFound($"Nenhuma reserva encontrada com o ID: {id}.");
            }
            return Ok(reservationConverter.ConvertFromModel(reservation));
        }

        [HttpGet("user/{userId:long}", Name = "FindReservationByUserId")]
        public async Task<IActionResult> FindByUserId(long userId)
        {
            var reservations = await reservationService.FindByUserId(userId);
            if (reservations == null || !reservations.Any())
            {
                return NotFound($"Nenhuma reserva encontrada com o ID do usuário: {userId}.");
            }

            var reservationsDto = new List<ReservationResultDto>();
            foreach (var reservation in reservations)
            {
                var loan = await serviceDataLookUp.LookUp(reservation);
                var reservationDto = reservationConverter.ConvertFromModel(reservation);
                reservationDto.Loan = loanConverter.ConvertFromModel(loan);
                reservationsDto.Add(reservationDto);
            }
            return Ok(reservationsDto);
        }

        [HttpGet("status/{statusName}", Name = "FindReservationsByStatus")]
        public async Task<IActionResult> FindReservationsByStatus(string statusName)
        {
            ReservationStatus reservationStatus;
            if (!Enum.TryParse<ReservationStatus>(statusName, out reservationStatus)) 
            {
                return BadRequest($"Não foi possível converter o status [{statusName}]");
            }
            var reservations = await reservationService.FindByStatus(reservationStatus);
            if (reservations == null || !reservations.Any())
            {
                return NotFound($"Nenhuma reserva encontrada com o status: {reservationStatus}.");
            }
            return Ok(reservations.Select(r => reservationConverter.ConvertFromModel(r)));
        }

        [HttpGet("orders/{statusName}", Name = "FindOrdersByStatus")]
        public async Task<IActionResult> FindOrdersByStatus(string statusName)
        {
            ReservationStatus reservationStatus;
            if (!Enum.TryParse<ReservationStatus>(statusName, out reservationStatus)) 
            {
                return BadRequest($"Não foi possível converter o status [{statusName}]");
            }
            var reservations = await reservationService.FindByStatus(reservationStatus);
            if (reservations == null || !reservations.Any())
            {
                return NotFound($"Nenhuma reserva encontrada com o status: {reservationStatus}.");
            }
            var orderList = new List<OrderDto>();
            var reservationResultList = reservations.Select(r => reservationConverter.ConvertFromModel(r));
            foreach (var reservationResult in reservationResultList) 
            {
                var order = new OrderDto();
                order.Reservation = reservationResult;
                order.Book = bookConverter.ConvertFromModel(await bookService.FindById(reservationResult.BookId));
                order.User = userConverter.ConvertFromModel(await userService.FindById(reservationResult.UserId));
                orderList.Add(order);
            }
            return Ok(orderList);
        }

        [HttpPost("order")]
        public async Task<IActionResult> Order([FromBody]ReservationRequestDto value)
        {
            var user = await userService.FindById(value.UserId);
            if (user == null)
            {
                return NotFound($"Nenhum usuário encontrado com o ID: {value.UserId}.");
            }

            var book = await bookService.FindById(value.BookId);
            if (book == null)
            {
                return NotFound($"Nenhum livro encontrado com o ID: {value.BookId}.");
            }

            return Ok(reservationConverter.ConvertFromModel(
                await reservationService.Order(book, user)
            ));
        }

        [HttpPost("approved/{id:long}")]
        public async Task<IActionResult> UpdateLoanStatusToApproved(long id)
        {
            var reservation = await reservationService.FindById(id);
            if (reservation == null)
            {
                return NotFound($"Nenhuma reserva encontrada com o ID: {id}.");
            }
            try
            {
                var book = await bookService.FindById(reservation.BookId);
                if (book == null)
                {
                    return NotFound($"Nenhum livro encontrado com o ID: {reservation.BookId}.");
                }
                await reservationService.ApprovedReservation(reservation, book);
                return Ok();
            }
            catch (ReservationApprovedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("rejected")]
        public async Task<IActionResult> UpdateLoanStatusToRejected([FromBody]ReservationRejectDto value)
        {
            var reservation = await reservationService.FindById(value.Id);
            if (reservation == null)
            {
                return NotFound($"Nenhuma reserva encontrada com o ID: {value.Id}.");
            }
            reservation.Reason.Description = value.Description;
            try
            {
                await reservationService.RejectedReservation(reservation);
                return Ok();
            }
            catch (ReservationApprovedException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
