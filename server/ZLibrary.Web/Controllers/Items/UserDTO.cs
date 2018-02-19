using System.Runtime.Serialization;

namespace ZLibrary.Web.Controllers.Items
{
    public class UserDTO
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "isAdministrator")]
        public bool IsAdministrator { get; set; }
        [DataMember(Name = "accessToken")]
        public string AccessToken { get; set; }
        [DataMember(Name = "userAvatarUrl")]
        public string UserAvatarUrl { get; set; }
    }
}