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

        [HttpGet("{id}", Name = "FindAuthor")]
        public async Task<IActionResult> FindById(long id)
        {
            var author = await authorService.FindById(id);
            if (author == null)
                return NotFound();
            return Ok(author);
        }

        [HttpDelete("{id}", Name = "DeleteAuthor")]
        public async Task<IActionResult> Delete(long id)
        {
            await authorService.Delete(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Author value)
        {
            var id = await authorService.Create(value);
            return CreatedAtRoute("FindAuthor", new { id }, id);
        }

    }
}
