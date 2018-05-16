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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using ZLibrary.Web.Utils;
using ZLibrary.Web.LookUps;

namespace ZLibrary.Web
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private const int MaxBoundaryLengthLimit = 70;

        private readonly IBookFacade bookFacade;
        private readonly IAuthorService authorService;
        private readonly IPublisherService publisherService;
        private readonly IReservationService reservationService;
        private readonly ILoanService loanService;
        private readonly IServiceDataLookUp serviceDataLookUp;

        public BooksController(IBookFacade bookFacade, IAuthorService authorService, IPublisherService publisherService, IReservationService reservationService, ILoanService loanService)
        {
            this.bookFacade = bookFacade;
            this.authorService = authorService;
            this.publisherService = publisherService;
            this.reservationService = reservationService;
            this.loanService = loanService;
            this.serviceDataLookUp = new DefaultServiceDataLookUp(loanService, reservationService);
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var books = await bookFacade.FindAll();
            return Ok(await books.ToBookViewItems(serviceDataLookUp));
        }

        [HttpGet("{id:long}", Name = "FindBook")]
        public async Task<IActionResult> FindById(long id)
        {
            var book = await bookFacade.FindById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(await book.ToBookViewItem(serviceDataLookUp));
        }

        [HttpDelete("{id:long}", Name = "DeleteBook")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await bookFacade.Delete(id);
                return NoContent();
            }
            catch (BookDeleteException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save()
        {
            string targetFilePath = null;
            BookDTO dto = null;

            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest($"Requisição precisa ser 'multipart'.");
            }

            var boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType), MaxBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);
            var section = await reader.ReadNextSectionAsync();
            while (section != null)
            {
                ContentDispositionHeaderValue contentDisposition;
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);
                var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                if (hasContentDispositionHeader)
                {
                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        if (key.ToString() == "file")
                        {
                            var imageValidator = new ImageValidator();
                            var fileName = HeaderUtilities.RemoveQuotes(contentDisposition.FileName).ToString();
                            var imageValidationResult = imageValidator.Validate(fileName);

                            if (imageValidationResult.HasError)
                            {
                                return BadRequest(imageValidationResult.ErrorMessage);
                            }

                            targetFilePath = Path.GetTempFileName();
                            using (var targetStream = System.IO.File.Create(targetFilePath))
                            {
                                await section.Body.CopyToAsync(targetStream);
                            }
                        }
                    }
                    else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                    {
                        if (key.ToString() == "value")
                        {
                            using (var streamReader = new StreamReader(section.Body))
                            {
                                var json = await streamReader.ReadToEndAsync();
                                dto = JsonConvert.DeserializeObject<BookDTO>(json);
                            }
                        }

                    }
                }
                section = await reader.ReadNextSectionAsync();
            }
            var validationContext = new ValidationContext(bookFacade, authorService, publisherService);
            var bookValidator = new BookValidator(validationContext);
            var validationResult = bookValidator.Validate(dto);

            if (validationResult.HasError)
            {
                return BadRequest(validationResult.ErrorMessage);
            }

            var bookFound = await bookFacade.FindById(dto.Id);
            if (bookFound == null && dto.Id != 0)
            {
                return NotFound($"Nenhum livro encontrado com o ID: {dto.Id}.");
            }

            try
            {
                var book = dto.FromBookViewItem(new DefaultValidationResultDataLookUp(validationResult));
                
                book = await bookFacade.Save(book, targetFilePath);

                return Ok(await book.ToBookViewItem(serviceDataLookUp));
            }
            catch (BookSaveException ex)
            {
                try
                {
                    System.IO.File.Delete(targetFilePath);
                }
                catch
                {
                    //Ignore
                }
                return BadRequest(ex.Message);
            }
            catch (ImageSaveException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("search/", Name = "FindBookBy")]
        public async Task<IActionResult> FindBy([FromBody]SearchParametersDTO value)
        {
            var orderBy = (SearchOrderBy)Enum.ToObject(typeof(SearchOrderBy), value.OrderByValue);
            var bookSearchParameter = new BookSearchParameter(value.Keyword)
            {
                OrderBy = orderBy
            };
            var books = await bookFacade.FindBy(bookSearchParameter);
            return Ok(await books.ToBookViewItems(serviceDataLookUp));
        }
    }
}