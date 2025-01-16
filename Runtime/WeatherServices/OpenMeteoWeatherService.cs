namespace WeatherServices
{
    public class OpenMeteoWeatherService : IWeatherService
    {
        private static string BaseUrl => "https://api.open-meteo.com/v1/forecast";

        public string BuildUrl(double latitude, double longitude)
        {
            return $"{BaseUrl}?latitude={latitude:F2}&longitude={longitude:F2}&hourly=temperature_2m";
        }
    }
}