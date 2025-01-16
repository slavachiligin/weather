namespace WeatherServices
{
    public interface IWeatherService
    {
        string BuildUrl(double latitude, double longitude);
    }
}