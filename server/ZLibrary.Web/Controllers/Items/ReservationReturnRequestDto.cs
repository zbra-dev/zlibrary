using System.Runtime.Serialization;

namespace ZLibrary.Web.Controllers.Items
{
    public class ReservationReturnRequestDto
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }
    }
}
