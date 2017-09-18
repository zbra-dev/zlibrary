using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;
using System.Collections.Generic;
using System.Linq;
using ZLibrary.Web.Controllers.Items;
using ZLibrary.Web.Validators;
using ZLibrary.Web.Extensions;
using System;
using ZLibrary.API.Exception;

namespace ZLibrary.Web
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly IPublisherService publisherService;

        public BooksController(IBookService bookService, IAuthorService authorService, IPublisherService publisherService)
        {
            this.bookService = bookService;
            this.authorService = authorService;
            this.publisherService = publisherService;
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
            return Ok(book.ToBookViewItem());
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
            var validationContext = new ValidationContext(bookService, authorService, publisherService);
            var bookValidator = new BookValidator(validationContext);
            var validationResult = bookValidator.Validate(value);

            if (validationResult.HasError)
            {
                return BadRequest(validationResult.ErrorMessage);
            }

            var book = await bookService.FindById(value.Id);

            if (book == null && value.Id != 0)
            {
                return BadRequest("Livro não cadastrado.");
            }

            if (book == null)
            {
                book = new Book();
            }
            book.Title = value.Title;
            book.Synopsis = value.Synopsis;
            book.PublicationYear = value.PublicationYear;
            book.Isbn = validationResult.GetResult<Isbn>();
            book.Publisher = validationResult.GetResult<Publisher>();
            book.Authors = validationResult.GetResult<List<BookAuthor>>();
            book.NumberOfCopies = value.NumberOfCopies;

            foreach (var bookAuthor in book.Authors)
            {
                bookAuthor.Book = book;
                bookAuthor.BookId = book.Id;
            }
            //TODO: CoverImage

            try
            {
                await bookService.Save(book);

                return Ok(book.ToBookViewItem());
            }
            catch (BookSaveException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{keyword}", Name = "FindBookBy")]
        public async Task<IActionResult> FindBy(string keyword)
        {
            var bookSearchParameter = new BookSearchParameter(keyword);
            var books = await bookService.FindBy(bookSearchParameter);
            return Ok(books.ToBookViewItems());
        }
    }
}
