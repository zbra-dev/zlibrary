using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ZLibrary.API;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;
using ZLibrary.Web.Extensions;
using ZLibrary.Web.Options;
using ZLibrary.Web.Validators;

namespace ZLibrary.Web.Controllers
{
    [Route("api/auth")]
    public class AuthenticationsController : Controller
    {
        private readonly IUserService userService;

        private readonly IAuthenticationApi authenticationApi;

        private readonly ClientOptions clientOptions;

        public AuthenticationsController(IUserService userService, IAuthenticationApi authenticationApi, ClientOptions clientOptions)
        {
            this.userService = userService;
            this.authenticationApi = authenticationApi;
            this.clientOptions = clientOptions;
        }

        [HttpGet("redirect/{code?}")]
        public async Task<IActionResult> RedirectFromSlack(string code)
        {
            using (var client = new HttpClient())
            {
                var httpMessage = await client.GetAsync(authenticationApi.GetAuthenticationUrl(code));
                var result = await httpMessage.Content.ReadAsStringAsync();
                using (httpMessage)
                using (var textReader = new StringReader(result))
                using (var reader = new JsonTextReader(textReader))
                {
                    var slackUserDTO = new JsonSerializer().Deserialize<SlackUserDTO>(reader);
                    var validator = new SlackAuthenticationDataValidator();
                    var validationResult = validator.Validate(slackUserDTO);
                    if (validationResult.HasError)
                    {
                        var errorMessage = validationResult.ErrorMessage;
                        Response.Cookies.Append("loginError", errorMessage);
                        return Redirect($"{clientOptions.ClientUrl}");
                    }

                    var user = await userService.FindByEmail(slackUserDTO.User.Email);
                    if (user == null)
                    {
                        user = new User()
                        {
                            Name = slackUserDTO.User.Name,
                            Email = slackUserDTO.User.Email,
                            AccessToken = slackUserDTO.AccessToken,
                            UserAvatarUrl = slackUserDTO.User.UserAvatarUrl
                        };
                        await userService.Create(user);
                    } 
                    else
                    {
                        user.AccessToken = slackUserDTO.AccessToken;
                        if(!string.IsNullOrEmpty(slackUserDTO.User.UserAvatarUrl)){
                            var imageurls = slackUserDTO.User.UserAvatarUrl.Split("https");
                            user.UserAvatarUrl = "https" + Uri.UnescapeDataString(imageurls.FirstOrDefault(url => url.Contains(".png")));
                        }
                        await userService.Update(user);
                    }

                    Response.Cookies.Append("user", JsonConvert.SerializeObject(user.ToUserViewItem()));
                    return Redirect($"{clientOptions.ClientUrl}/books");
                }
            }
        }
    }
}