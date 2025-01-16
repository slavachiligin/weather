using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherServices;

public class WeatherServiceProvider : IWeatherProvider
{
    private readonly List<IWeatherService> _services = new List<IWeatherService>();
    
    public void AddService(IWeatherService service)
    {
        _services.Add(service);
    }

    public async Task<IReadOnlyList<string>> GetWeather(double latitude, double longitude, float timeout, CancellationToken cancellationToken)
    {
        using var timeoutCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeoutCancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(timeout));
        var timeoutCancellationToken = timeoutCancellationTokenSource.Token;
        
        var weatherData = new List<string>(_services.Count);
        foreach (var service in _services)
        {
            var weatherResult = await WebRequestSender.SendWeatherRequest(service.BuildUrl(latitude, longitude), timeoutCancellationToken);
            weatherData.Add(weatherResult);
        }

        return weatherData;
    }
}