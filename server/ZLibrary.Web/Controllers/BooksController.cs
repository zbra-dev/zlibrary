using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;

namespace ZLibrary.Web
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var books = await bookService.FindAll();
            return Ok(books);
        }

        [HttpGet("{id}", Name = "FindBook")]
        public async Task<IActionResult> FindById(long id)
        {
            var book = await bookService.FindById(id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpDelete("{id}", Name = "DeleteBook")]
        public async Task<IActionResult> Delete(long id)
        {
            await bookService.Delete(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Book value)
        {
            var id = await bookService.Create(value);
            return CreatedAtRoute("FindBook", new { id }, id);
        }
    }
}
