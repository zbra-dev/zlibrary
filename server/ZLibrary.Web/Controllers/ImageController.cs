using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZLibrary.API;

namespace ZLibrary.Web.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file) 
        {
            var filePath = Path.GetTempFileName();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    using (var binaryReader = new BinaryReader(stream))
                    {
                        var fileContent = binaryReader.ReadBytes((int)file.Length);
                        await imageService.SaveImage(fileContent, file.ContentType);
                    }
                }
            }

            return Ok();
        }
    }
}