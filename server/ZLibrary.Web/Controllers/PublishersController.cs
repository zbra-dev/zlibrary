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

namespace ZLibrary.Web
{
    [Route("api/[controller]")]
    public class PublishersController : Controller
    {
        private readonly IPublisherService publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        [HttpGet("{name}", Name = "FindPublisherByName")]
        public async Task<IActionResult> FindByName(string name)
        {
            var Publisher = await publisherService.FindByName(name);
            return Ok(Publisher.ToPublisherViewItems());
        }
    }
}
