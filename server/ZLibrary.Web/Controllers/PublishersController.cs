using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;

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

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var publishers = await publisherService.FindAll();
            return Ok(publishers);
        }
    }
}
