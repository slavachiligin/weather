namespace WeatherServices
{
    public class OpenWeatherMapWeatherService : IWeatherService
    {
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";
        private const string ApiKey = "74032c96516f1a0258464148f4b04f98";

        public string BuildUrl(double latitude, double longitude)
        {
            return $"{BaseUrl}?lat={latitude}&lon={longitude}&units=metric&appid={ApiKey}";
        }
    }
}