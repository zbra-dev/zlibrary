using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;

namespace ZLibrary.Web
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookService service;

        public BooksController(IBookService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var books = await service.FindAll();
            return Ok(books);
        }

        [HttpGet("{id}", Name = "FindBook")]
        public async Task<IActionResult> FindById(long id)
        {
            var book = await service.FindById(id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Book value)
        {
            var id = await service.Create(value);
            return CreatedAtRoute("FindBook", new { id }, id);
        }

    }
}
