using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using System.Collections.Generic;
using System.Linq;
using ZLibrary.Web.Controllers.Items;
using ZLibrary.Web.Validators;
using ZLibrary.Web.Extensions;
using Microsoft.AspNetCore.Http;
using System.IO;

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
            return Ok(books.ToBookViewItems());
        }

        [HttpGet("{id:long}", Name = "FindBook")]
        public async Task<IActionResult> FindById(long id)
        {
            var book = await bookService.FindById(id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpDelete("{id:long}", Name = "DeleteBook")]
        public async Task<IActionResult> Delete(long id)
        {
            await bookService.Delete(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody]BookDTO value)
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
            book.Authors = validationResult.GetResult<List<BookAuthor>>();

            var id = await bookService.Save(book);
            
            return Ok(BookDTO.FromModel(book));
        }


       [HttpGet("{keyword}", Name = "FindBookBy")]
       public async Task<IActionResult> FindBy(string keyword)
       {
           var bookSearchParameter =  new BookSearchParameter(keyword);
           var books = await bookService.FindBy(bookSearchParameter);
           return Ok(books.ToBookViewItems());
       }
    }
}
