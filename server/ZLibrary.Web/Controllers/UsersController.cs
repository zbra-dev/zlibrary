using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using ZLibrary.Web.Converters;

namespace ZLibrary.Web
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly UserConverter userConverter;

        public UsersController(IUserService userService, UserConverter userConverter)
        {
            this.userService = userService;
            this.userConverter = userConverter;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var users = await userService.FindAll();
            return Ok(users);
        }

        [HttpGet("{id}", Name = "FindUser")]
        public async Task<IActionResult> FindById(long id)
        {
            var user = await userService.FindById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(userConverter.ConvertFromModel(user));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]User value)
        {
            var id = await userService.Create(value);
            return CreatedAtRoute("FindUser", new { id }, id);
        }
    }
}
