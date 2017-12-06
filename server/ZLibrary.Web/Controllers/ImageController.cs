using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZLibrary.API;
using ZLibrary.Web.Validators;

namespace ZLibrary.Web.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IImageService imageService;
        private readonly IBookService bookService;

        public ImageController(IImageService imageService, IBookService bookService)
        {
            this.imageService = imageService;
            this.bookService = bookService;
        }

        [HttpPost("Upload/")]
        public async Task<IActionResult> Upload(Guid key, IFormFile file)
        {
            var filePath = Path.GetTempFileName();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    using (var binaryReader = new BinaryReader(stream))
                    {
                        var fileContent = binaryReader.ReadBytes((int)file.Length);

                        var imageValidator = new ImageValidator();
                        var validationResult = imageValidator.Validate(file);

                        if (validationResult.HasError)
                        {
                            return BadRequest(validationResult.ErrorMessage);
                        }
                        await imageService.SaveImage(key, fileContent);
                    }
                }
            }

            return Ok(key);
        }

        [HttpGet("LoadImage/{key}")]
        public async Task<IActionResult> LoadImage(Guid key)
        {

            var book = await bookService.FindByCoverImageKey(key);

            if (book == null)
            {
                return BadRequest("Não foi encontrado nenhum livro com está imagem.");
            }

            var imageArray = await imageService.LoadImage(key);
            if (!imageArray.Any())
            {
                return BadRequest($"Nenhum arquivo encontrado com a chave: {key}");
            }

            return Ok(imageArray);
        }
    }
}