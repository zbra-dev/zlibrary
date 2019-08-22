using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZLibrary.API;
using System.Linq;
using ZLibrary.Web.Converters;
using ZLibrary.Web.Converters;
using ZLibrary.Web.Controllers.Items;
using ZLibrary.Web.Validators;

namespace ZLibrary.Web
{
    [Route("api/[controller]")]
    public class PublishersController : Controller
    {
        private readonly IPublisherService publisherService;
        private readonly PublisherConverter publisherConverter;
        private readonly PublisherDtoValidator publisherDtoValidator;

        public PublishersController(IPublisherService publisherService, PublisherConverter publisherConverter,
            PublisherDtoValidator publisherDtoValidator)
        {
            this.publisherService = publisherService;
            this.publisherConverter = publisherConverter;
            this.publisherDtoValidator = publisherDtoValidator;
        }

        [HttpGet("{name}", Name = "FindPublisherByName")]
        public async Task<IActionResult> FindByName(string name)
        {
            var publishers = await publisherService.FindByName(name);
            return Ok(publishers.Select(p => publisherConverter.ConvertFromModel(p)));
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody]PublisherDto dto)
        {
            var validationResult = publisherDtoValidator.Validate(dto);
            if (validationResult.HasError)
            {
                return BadRequest(validationResult.ErrorMessage);
            }
            
            var publisherSaved = publisherConverter.ConvertFromModel(
                await publisherService.Save(publisherConverter.ConvertToModel(dto))
            );

            return Ok(publisherSaved);
        }
    }
}
