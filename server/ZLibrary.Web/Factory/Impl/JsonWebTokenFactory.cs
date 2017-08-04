using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ZLibrary.Model;
using ZLibrary.Web.Options;
using ZLibrary.Web.Resources;
using Newtonsoft.Json;

namespace ZLibrary.Web.Factory.Impl
{
    public class JsonWebTokenFactory : ITokenFactory
    {
        private readonly JwtOptions jwtOptions;

        public JsonWebTokenFactory(IOptions<JwtOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions.Value;
        }
        
        public TokenResource CreateToken(User user)
        {
            var now = DateTime.UtcNow;
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N").ToUpper()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.Role, BuildRoles(), JsonClaimValueTypes.JsonArray)
            };

            var jwt = new JwtSecurityToken(
                jwtOptions.Issuer,
                jwtOptions.Audience,
                claims,
                now,
                jwtOptions.Expires.HasValue ? now.Add(jwtOptions.Expires.Value) : (DateTime?)null,
                jwtOptions.SigningCredentials);
            
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new TokenResource()
            {
                Token = encodedJwt,
                ExpiresIn = jwtOptions.Expires?.TotalSeconds
            };
        }

        // TODO: Example only, add proper roles here
        private string BuildRoles()
        {
            var roles = new[] { "User" };
            return JsonConvert.SerializeObject(roles);
        }
    }
}
