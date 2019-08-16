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
using ZLibrary.Web.Utils;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.WebUtilities;
using ZLibrary.Web.Converters;

namespace ZLibrary.Web
{
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService authorService;
        private readonly AuthorConverter authorConverter;
        private readonly AuthorDTOValidator authorValidator;

        public AuthorsController(IAuthorService authorService, AuthorConverter authorConverter, AuthorDTOValidator authorValidator)
        {
            this.authorService = authorService;
            this.authorConverter = authorConverter;
            this.authorValidator = authorValidator;
        }

        [HttpGet("{name}", Name = "FindAuthorByName")]
        public async Task<IActionResult> FindByName(string name)
        {
            var authors = await authorService.FindByName(name);
            return Ok(authors.ToAuthorViewItems());
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] AuthorDTO dto)
        {

            var validationResult = authorValidator.Validate(dto);
            if (validationResult.HasError)
            {
                return BadRequest(validationResult.ErrorMessage);
            }
            
            var authorSaved = await authorService.Save(authorConverter.ConvertToModel(dto));
            return Ok(authorSaved.ToAuthorViewItem());
        }
    }
}
