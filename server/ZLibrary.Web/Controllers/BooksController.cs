using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using System.Collections.Generic;
using ZLibrary.Web.Validators;

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
        public async Task<IActionResult> Create([FromBody]BookDTO value)
        {
            var validationContext = new ValidationContext();
            var bookValidator = new BookValidator(validationContext);
            var validationResult = bookValidator.Validate(value);

            if (validationResult.HasError) 
            {
                return BadRequest(validationResult.ErrorMessage);
            }

            var book = await bookService.FindById(value.Id);
            if (book == null)
            {
                book = new Book();
            }
            book.Publisher = validationResult.GetResult<Publisher>();
            book.Authors = validationResult.GetResult<List<Author>>();

            var id = await bookService.Create(book);
            
            return Ok(BookDTO.FromModel(book));
        }
    }
}
