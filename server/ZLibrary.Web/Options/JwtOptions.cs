using Microsoft.IdentityModel.Tokens;
using System;

namespace ZLibrary.Web.Options
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Authority { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public TimeSpan? Expires { get; set; }
    }
}
