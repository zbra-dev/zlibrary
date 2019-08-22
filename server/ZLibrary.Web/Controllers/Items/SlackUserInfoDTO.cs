using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZLibrary.Web.Controllers.Items
{
    public class SlackUserInfoDto
    { 
       [DataMember(Name = "email")]
       public string Email { get; set; }

       [DataMember(Name = "name")]
       public string Name { get; set; }

       [DataMember(Name = "userAvatarUrl")]
       [JsonProperty("image_512")]
       public string UserAvatarUrl { get; set; }
    }
}