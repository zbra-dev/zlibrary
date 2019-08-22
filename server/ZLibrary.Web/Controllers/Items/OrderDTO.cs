using System;
using System.Runtime.Serialization;
using ZLibrary.Model;

namespace ZLibrary.Web.Controllers.Items
{
    public class OrderDto
    {
        [DataMember(Name = "reservation")]
        public ReservationResultDto Reservation { get; set; }

        [DataMember(Name = "book")]
        public BookDto Book { get; set; }

        [DataMember(Name = "user")]
        public UserDto User { get; set; }
    }
}