using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ZLibrary.Web.Controllers
{
    [Route("api/auth")]
    public class AuthenticationsController : Controller
    {
        [HttpGet("redirect/{code?}")]
        public async Task<IActionResult> RedirectFromSlack(string code)
        {
            using (HttpClient client = new HttpClient())
            {
                // TODO: load slack auth configuration from file
                var SlackApiBase = "https://slack.com/api/{0}";
                var clientId = "6575166742.236473063552";
                var clientSecret = "e33f0fbba6c17d24ecae452a9f146b8c";
                var queryString = $"client_id={clientId}&client_secret={clientSecret}&code={code}";
                var httpMessage = await client.GetAsync(string.Format(SlackApiBase, "oauth.access") + "?" + queryString);
                using (httpMessage)
                using (var textReader = new StringReader(await httpMessage.Content.ReadAsStringAsync()))
                using (var reader = new JsonTextReader(textReader))
                {
                    // FIXME: parse string to SlackUserDTO
                    var responseString = JsonSerializer.Create().Deserialize(reader);
                    Console.WriteLine(responseString);

                    // TODO: update User entity model on database and include AccessToken and UserAvatarUrl
                }
            }

            return Ok();
        }
    }
}