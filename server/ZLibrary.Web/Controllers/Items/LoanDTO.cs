using System;
using System.Runtime.Serialization;
using ZLibrary.Model;

namespace ZLibrary.Web.Controllers.Items
{
    public class LoanDto
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }
        [DataMember(Name = "reservationId")]
        public long ReservationId { get; set; }
        [DataMember(Name = "expirationDate")]
        public DateTime ExpirationDate { get; set; }
        [DataMember(Name = "endDate")]
        public DateTime EndDate { get; set; }
        [DataMember(Name = "startDate")]
        public DateTime StartDate { get; set; }
        [DataMember(Name = "isExpired")]
        public bool IsExpired { get; set; }
         [DataMember(Name = "isReturned")]
        public bool IsReturned { get; set; }
        [DataMember(Name = "canBorrow")]
        public bool CanBorrow { get; set; }
    }
}