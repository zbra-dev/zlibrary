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

        [HttpGet("{id}", Name = "FindPublisher")]
        public async Task<IActionResult> FindById(long id)
        {
            var publisher = await publisherService.FindById(id);
            if (publisher == null)
                return NotFound();
            return Ok(publisher);
        }

        [HttpDelete("{id}", Name = "DeletePublisher")]
        public async Task<IActionResult> Delete(long id)
        {
            await publisherService.Delete(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Publisher publisher)
        {
            var id = await publisherService.Create(publisher);
            return CreatedAtRoute("FindPublisher", new { id }, id);
        }

    }
}
