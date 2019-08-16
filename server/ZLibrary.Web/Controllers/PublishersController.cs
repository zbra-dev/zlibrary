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
using ZLibrary.Web.Converters;

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
            var publisher = await publisherService.FindByName(name);
            return Ok(publisher.ToPublisherViewItems());
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody]PublisherDTO dto)
        {
            var publisherSaved = await publisherService.Save(publisherConverter.ConvertToModel(dto));
            return Ok(publisherConverter.ConvertFromModel(publisherSaved));
        }
    }
}
