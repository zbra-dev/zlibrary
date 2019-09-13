using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Converters
{
    public class OrderConverter : AbstractFromModelConverter<Reservation, OrderDto>
    {

        private readonly BookConverter bookConverter;
        private readonly UserConverter userConverter;
        private readonly ReservationConverter reservationConverter;

        public OrderConverter(BookConverter bookConverter, UserConverter userConverter, ReservationConverter reservationConverter)
        {
            this.bookConverter = bookConverter;
            this.userConverter = userConverter;
            this.reservationConverter = reservationConverter;
        }

        protected override OrderDto NullSafeConvertFromModel(Reservation model)
        {
            return new OrderDto
            {
                Reservation = reservationConverter.ConvertFromModel(model),
                Book = bookConverter.ConvertFromModel(model.Book),
                User = userConverter.ConvertFromModel(model.User),
            };
        }
    }
}
