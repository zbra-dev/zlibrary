using System.Runtime.Serialization;

namespace ZLibrary.Web.Controllers.Items
{
    public class AuthorDto
    {
        [DataMember(Name = "id")]
        public long? Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}