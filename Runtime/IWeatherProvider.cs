using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherServices;

public interface IWeatherProvider
{
    void AddService(IWeatherService service);
    Task<IReadOnlyList<string>> GetWeather(double latitude, double longitude, float timeout, CancellationToken cancellationToken);
}