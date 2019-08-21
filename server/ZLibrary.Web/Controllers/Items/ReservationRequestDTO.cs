using System.Runtime.Serialization;

namespace ZLibrary.Web.Controllers.Items
{
    public class ReservationRequestDto
    {
        [DataMember(Name = "userId")]
        public long UserId { get; set; }
        [DataMember(Name = "bookId")]
        public long BookId { get; set; }
    }
}