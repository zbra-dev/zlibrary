using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;

namespace ZLibrary.Web
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var users = await service.FindAll();
            return Ok(users);
        }

        [HttpGet("{id}", Name = "FindUser")]
        public async Task<IActionResult> FindById(long id)
        {
            var user = await service.FindById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]User value)
        {
            var id = await service.Create(value);
            return CreatedAtRoute("FindUser", new { id }, id);
        }
    }
}
