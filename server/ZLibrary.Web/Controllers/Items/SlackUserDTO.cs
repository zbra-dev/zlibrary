using System;
using System.Linq;
using System.Runtime.Serialization;

namespace ZLibrary.Web.Controllers.Items
{
    [DataContract (Name = "SlackUserDTO")]
    public class SlackUserDTO
    {
        private const string ImageProtocol = "https";
        private const string ImageExtension = ".png";

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

        internal string GetUserAvatarUrl()
        {
            if (!string.IsNullOrEmpty(User.UserAvatarUrl))
            {
                var imageURLs = User.UserAvatarUrl.Split(ImageProtocol);
                return string.Concat(ImageProtocol, Uri.UnescapeDataString(imageURLs.FirstOrDefault(url => url.Contains(ImageExtension))));
            }
            return string.Empty;
        }
    }
}