using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZLibrary.API;
using System.Linq;
using ZLibrary.Web.Converters;
using ZLibrary.Web.Converters;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web
{
    [Route("api/[controller]")]
    public class PublishersController : Controller
    {
        private readonly IPublisherService publisherService;
        private readonly PublisherConverter publisherConverter;

        public PublishersController(IPublisherService publisherService, PublisherConverter publisherConverter)
        {
            this.publisherService = publisherService;
            this.publisherConverter = publisherConverter;
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
            var publisherSaved = await publisherService.Save(publisherConverter.ConvertToModel(dto));
            return Ok(publisherConverter.ConvertFromModel(publisherSaved));
        }
    }
}
