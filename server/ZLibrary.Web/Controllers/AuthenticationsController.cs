using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZLibrary.API;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;
using ZLibrary.Web.Options;
using ZLibrary.Web.Validators;
using ZLibrary.Web.Converters;

namespace ZLibrary.Web.Controllers
{
    [Route("api/auth")]
    public class AuthenticationsController : Controller
    {
        private readonly IUserService userService;
        private readonly IAuthenticationApi authenticationApi;
        private readonly ClientOptions clientOptions;
        private readonly UserConverter userConverter;

        public AuthenticationsController(IUserService userService, IAuthenticationApi authenticationApi, ClientOptions clientOptions, UserConverter userConverter)
        {
            this.userService = userService;
            this.authenticationApi = authenticationApi;
            this.clientOptions = clientOptions;
            this.userConverter = userConverter;
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
                    var slackUserDTO = new JsonSerializer().Deserialize<SlackUserDto>(reader);
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
                        user.UserAvatarUrl = slackUserDTO.GetUserAvatarUrl();
                        await userService.Update(user);
                    }

                    Response.Cookies.Append("user", JsonConvert.SerializeObject(userConverter.ConvertFromModel(user)));
                    return Redirect($"{clientOptions.ClientUrl}/books");
                }
            }
        }
    }
}