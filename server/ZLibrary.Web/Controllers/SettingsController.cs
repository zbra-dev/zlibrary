using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ZLibrary.Core;

namespace ZLibrary.Web.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        private readonly FeatureSettings featureSettings;

        public SettingsController(IOptions<FeatureSettings> featureSettings)
        {
            this.featureSettings = featureSettings.Value;
        }

        [HttpGet("features")]
        public IActionResult GetFeatureSettings()
        {
            return Ok(featureSettings);
        }
    }
}