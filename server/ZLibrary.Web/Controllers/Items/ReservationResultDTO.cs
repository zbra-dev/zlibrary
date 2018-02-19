using System;
using System.Runtime.Serialization;

namespace ZLibrary.Web.Controllers.Items
{
    public class ReservationResultDTO
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }
        [DataMember(Name = "user")]
        public long UserId { get; set; }
        [DataMember(Name = "userId")]
        public long BookId { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "statusId")]
        public long StatusId { get; set; }
        [DataMember(Name = "startDate")]
        public DateTime StartDate { get; set; }
        [DataMember(Name = "loanStatusId")]
        public long LoanStatusId { get; set; }
    }
}