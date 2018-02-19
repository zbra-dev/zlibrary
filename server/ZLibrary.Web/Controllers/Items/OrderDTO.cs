using System;
using System.Runtime.Serialization;
using ZLibrary.Model;

namespace ZLibrary.Web.Controllers.Items
{
    public class OrderDTO
    {
        [DataMember(Name = "reservations")]
        public ReservationResultDTO[] Reservations { get; set; }
    }
}