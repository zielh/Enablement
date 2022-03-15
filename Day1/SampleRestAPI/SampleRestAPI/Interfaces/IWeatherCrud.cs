namespace SampleRestAPI.Interfaces
{
    public interface IWeatherCrud
    {
        bool AddSummaries(string summary);
        bool DeleteSummaries(int idx);
        IEnumerable<Models.WeatherForecast> GetSummaries();
        IEnumerable<string> GetSummariesText();
    }
}
