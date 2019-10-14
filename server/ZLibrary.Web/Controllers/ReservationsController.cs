using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using System.Linq;
using ZLibrary.Web.Controllers.Items;
using System;
using ZLibrary.Web.Converters;

namespace ZLibrary.Web
{
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {

        private readonly RichReservationConverter richReservationConverter;
        private readonly OrderConverter orderConverter;
        private readonly ReservationCancelRequestConverter reservationCancelRequestConverter;
        private readonly ReservationReturnRequestConverter reservationReturnRequestConverter;
        private readonly ReservationHoldRequestConverter reservationHoldRequestConverter;
        private readonly ReservationApproveRequestConverter reservationApproveRequestConverter;
        private readonly IReservationFacade reservationFacade;

        public ReservationsController(RichReservationConverter richReservationConverter,
            OrderConverter orderConverter,
            ReservationCancelRequestConverter reservationCancelRequestConverter,
            ReservationReturnRequestConverter reservationReturnRequestConverter,
            ReservationHoldRequestConverter reservationHoldRequestConverter,
            ReservationApproveRequestConverter reservationApproveRequestConverter,
            IReservationFacade reservationFacade)
        {
            this.richReservationConverter = richReservationConverter;
            this.orderConverter = orderConverter;
            this.reservationCancelRequestConverter = reservationCancelRequestConverter;
            this.reservationReturnRequestConverter = reservationReturnRequestConverter;
            this.reservationHoldRequestConverter = reservationHoldRequestConverter;
            this.reservationApproveRequestConverter = reservationApproveRequestConverter;
            this.reservationFacade = reservationFacade;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var reservations = await reservationFacade.FindAll();
            return Ok(reservations.Select(r => richReservationConverter.ConvertFromModel(r)).ToList());
        }

        [HttpGet("{id:long}", Name = "FindReservation")]
        public async Task<IActionResult> FindById(long id)
        {
            try
            {
                return Ok(richReservationConverter.ConvertFromModel(await reservationFacade.FindById(id)));
            }
            catch (ReservationNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("user/{userId:long}", Name = "FindReservationByUserId")]
        public async Task<IActionResult> FindByUserId(long userId)
        {
            var reservations = await reservationFacade.FindByUserId(userId);
            return Ok(reservations.Select(r => richReservationConverter.ConvertFromModel(r)).ToList());
        }

        [HttpGet("status/{statusName}", Name = "FindReservationsByStatus")]
        public async Task<IActionResult> FindReservationsByStatus(string statusName)
        {
            ReservationStatus reservationStatus;
            if (!Enum.TryParse<ReservationStatus>(statusName, out reservationStatus))
            {
                return BadRequest($"Não foi possível converter o status [{statusName}]");
            }

            var reservations = await reservationFacade.FindByStatus(reservationStatus);
            return Ok(reservations.Select(r => richReservationConverter.ConvertFromModel(r)).ToList());
        }

        [HttpGet("orders/{statusName}", Name = "FindOrdersByStatus")]
        public async Task<IActionResult> FindOrdersByStatus(string statusName)
        {
            ReservationStatus reservationStatus;
            if (!Enum.TryParse<ReservationStatus>(statusName, out reservationStatus))
            {
                return BadRequest($"Não foi possível converter o status [{statusName}]");
            }

            var reservations = await reservationFacade.FindByStatus(reservationStatus);
            return Ok(reservations.Select(r => orderConverter.ConvertFromModel(r.Reservation)));

        }

        [HttpGet("requested-orders")]
        public async Task<IActionResult> FindRequestedOrders()
        {
            var reservations = await reservationFacade.FindRequestedOrders();

            return Ok(reservations.Select(r => orderConverter.ConvertFromModel(r.Reservation)));
        }

        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder([FromBody]ReservationRequestDto value)
        {
            return Ok(richReservationConverter.ConvertFromModel(
                await reservationFacade.CreateOrder(value.BookId, value.UserId)
            ));
        }

        [HttpPost("approved")]
        public async Task<IActionResult> ApproveReservation([FromBody]ReservationApproveRequestDto reservationApproveRequestDto)
        {
            try
            {
                await reservationFacade.ApproveReservation(reservationApproveRequestConverter.ConvertToModel(reservationApproveRequestDto));
                return Ok();
            }
            catch (ReservationNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BookNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ReservationApprovedException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("waiting")]
        public async Task<IActionResult> HoldReservation([FromBody]ReservationHoldRequestDto reservationHoldRequestDto)
        {
            try
            {
                await reservationFacade.QueueReservation(reservationHoldRequestConverter.ConvertToModel(reservationHoldRequestDto));
                return Ok();
            }
            catch (ReservationNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("returned")]
        public async Task<IActionResult> ReturnReservation([FromBody]ReservationReturnRequestDto reservationReturnRequestDto)
        {
            try
            {
                await reservationFacade.ReturnReservation(reservationReturnRequestConverter.ConvertToModel(reservationReturnRequestDto));
                return Ok();
            }
            catch (ReservationNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BookNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ReservationApprovedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidLoanException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("canceled")]
        public async Task<IActionResult> CancelReservation([FromBody]ReservationCancelRequestDto reservationCancelRequestDto)
        {
            try
            {
                await reservationFacade.CancelReservation(reservationCancelRequestConverter.ConvertToModel(reservationCancelRequestDto));
                return Ok();
            }
            catch (ReservationNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}