using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ZLibrary.Web.Controllers.Items
{
    public class ReservationDto
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "name")]
        public string NewStatus { get; set; }

        [DataMember(Name = "bookId")]
        public long BookId { get; set; }
    }
}
