
namespace SampleRestAPI.Services
{
    public class WeatherCrud : Interfaces.IWeatherCrud
    {
        private static List<string> _Summaries = new List<string>()
           {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public IEnumerable<Models.WeatherForecast> GetSummaries()
        {
            if (_Summaries != null)
                return Enumerable.Range(1, 5).Select(index => new Models.WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = _Summaries[Random.Shared.Next(_Summaries.Count)]
                }).ToArray();

            return new List<Models.WeatherForecast>();
        }

        public IEnumerable<string> GetSummariesText()
        {
            return _Summaries;
        }


        public bool AddSummaries(string summary)
        {
            try
            {
                if (!_Summaries.Contains(summary))
                    _Summaries.Add(summary);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteSummaries(int idx)
        {
            try
            {
                _Summaries.RemoveAt(idx);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
