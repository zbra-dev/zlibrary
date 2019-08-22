using System.Runtime.Serialization;

namespace ZLibrary.Web.Controllers.Items
{
    public class ReservationRejectDto
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}