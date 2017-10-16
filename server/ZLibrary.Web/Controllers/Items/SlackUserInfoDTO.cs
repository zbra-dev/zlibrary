using System.Runtime.Serialization;

namespace ZLibrary.Web.Controllers.Items
{
    public class SlackUserInfoDTO
    { 
       [DataMember(Name = "email")]
       public string Email { get; set; }

       [DataMember(Name = "name")]
       public string Name { get; set; }

       [DataMember(Name = "userAvatarUrl")]
       public string UserAvatarUrl { get; set; }
    }
}