using Microsoft.AspNetCore.Mvc;
using upwork_scraper_server.services;

namespace upwork_scraper_server.controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        private readonly SettingsService _settingsService;

        public TestController(SettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet]
        public IActionResult Test()
        {
            _settingsService.IsActive();
            return Ok();
        }
    }
}