using System;
using System.Runtime.Serialization;
using ZLibrary.Model;

namespace ZLibrary.Web.Controllers.Items
{
    public class OrderDTO
    {
        [DataMember(Name = "reservation")]
        public ReservationResultDTO Reservation { get; set; }

        [DataMember(Name = "book")]
        public BookDTO Book { get; set; }

        [DataMember(Name = "user")]
        public UserDTO User { get; set; }
    }
}