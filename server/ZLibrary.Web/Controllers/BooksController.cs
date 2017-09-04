using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using System.Collections.Generic;
using System.Linq;
using ZLibrary.Web.DTO;

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
            return Ok(books.Select(b => new BookDTO() 
            {
                Id = b.Id,
                AuthorIds = b.Authors.Select(a => a.AuthorId).ToArray(),
                Isbn = b.Isbn.Value,
                PublisherId = b.Publisher.Id,
                PublicationYear = b.PublicationYear,
                Title = b.Title,
                Synopsis = b.Synopsis
            }));
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
        public async Task<IActionResult> Save([FromBody]Book value)
        {
            var id = await bookService.Save(value);
            return CreatedAtRoute("FindBook", new { id }, id);
        }


       [HttpGet("{keyword}", Name = "FindBookBy")]
       public async Task<IActionResult> FindBy(string keyword)
       {
           var bookSearchParameter =  new BookSearchParameter(keyword);
           var result = await bookService.FindBy(bookSearchParameter);
           return Ok(result);
       }
    }
}
