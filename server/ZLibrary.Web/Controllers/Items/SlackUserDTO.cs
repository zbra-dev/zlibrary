using System.Runtime.Serialization;

namespace ZLibrary.Web.Controllers.Items
{
    [DataContract (Name = "SlackUserDTO")]
    public class SlackUserDTO
    {
        [DataMember(Name = "ok")]
        public string Ok { get; set; }

        [DataMember(Name = "scope")]
        public string Scope { get; set; }
        
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "user")]
        public SlackUserInfoDTO User { get; set; }

        [DataMember(Name = "error")]
        public string Error { get; set; }
    }
}