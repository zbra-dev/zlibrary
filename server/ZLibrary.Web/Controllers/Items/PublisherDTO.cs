using System.Runtime.Serialization;
using ZLibrary.Model;

namespace ZLibrary.Web.Controllers.Items
{
    public class PublisherDto
    {
        [DataMember(Name = "id")]
        public long? Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}