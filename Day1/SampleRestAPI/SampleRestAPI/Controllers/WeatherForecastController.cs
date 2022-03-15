using Microsoft.AspNetCore.Mvc;

namespace SampleRestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        Services.WeatherCrud _weather = new Services.WeatherCrud();
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Models.WeatherForecast> Get()
        {
            return _weather.GetSummaries();
        }

        [HttpGet]
        [Route("GetText")]
        public IEnumerable<string> GetText()
        {
            return _weather.GetSummariesText();
        }

        [HttpPost]
        public IActionResult Post(string summary)
        {
            return Ok(_weather.AddSummaries(summary));
        }

        [HttpDelete]
        public IActionResult Delete(int idx)
        {
            return Ok(_weather.DeleteSummaries(idx));
        }
    }
}