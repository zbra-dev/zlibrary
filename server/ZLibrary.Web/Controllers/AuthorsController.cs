using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZLibrary.API;
using System.Linq;
using ZLibrary.Web.Controllers.Items;
using ZLibrary.Web.Validators;
using ZLibrary.Web.Converters;

namespace ZLibrary.Web
{
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService authorService;
        private readonly AuthorConverter authorConverter;
        private readonly AuthorDtoValidator authorValidator;

        public AuthorsController(IAuthorService authorService, AuthorConverter authorConverter, AuthorDtoValidator authorValidator)
        {
            this.authorService = authorService;
            this.authorConverter = authorConverter;
            this.authorValidator = authorValidator;
        }

        [HttpGet("{name}", Name = "FindAuthorByName")]
        public async Task<IActionResult> FindByName(string name)
        {
            var authors = await authorService.FindByName(name);

            return Ok(authors.Select(a => authorConverter.ConvertFromModel(a)));
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] AuthorDto dto)
        {

            var validationResult = authorValidator.Validate(dto);
            if (validationResult.HasError)
            {
                return BadRequest(validationResult.ErrorMessage);
            }
            
            var authorSaved = authorConverter.ConvertFromModel(
                await authorService.Save(authorConverter.ConvertToModel(dto))
            );

            return Ok(authorSaved);
        }
    }
}
