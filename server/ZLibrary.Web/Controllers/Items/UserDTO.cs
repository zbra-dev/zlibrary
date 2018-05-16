using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZLibrary.Web.Controllers.Items
{
    public class UserDTO
    {
        [DataMember(Name = "id")]
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "email")]
        [JsonProperty("email")]
        public string Email { get; set; }
        [DataMember(Name = "isAdministrator")]
        [JsonProperty("isAdministrator")]
        public bool IsAdministrator { get; set; }
        [DataMember(Name = "accessToken")]
        [JsonProperty("acessToken")]
        public string AccessToken { get; set; }
        [DataMember(Name = "userAvatarUrl")]
        [JsonProperty("userAvatarUrl")]
        public string UserAvatarUrl { get; set; }
    }
}