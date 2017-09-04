using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;

namespace ZLibrary.Web
{
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService authorService;

        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var authors = await authorService.FindAll();
            return Ok(authors);
        }
    }
}
