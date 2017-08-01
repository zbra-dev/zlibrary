using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;

namespace ZLibrary.Web
{
    public partial class Startup
    {
        private void ConfigureAuth(IApplicationBuilder app)
        {
            var jwtOptions = BuildJwtOptions();
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = jwtOptions.SigningCredentials.Key,
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,
                RequireExpirationTime = false
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });
        }
    }
}
