using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LocationService;

public class LocationWeatherServiceProvider : WeatherServiceProvider, ILocationWeatherProvider
{
    private readonly ILocationService _locationService;

    public LocationWeatherServiceProvider(ILocationService locationService)
    {
        _locationService = locationService;
    }

    public async Task<IReadOnlyList<string>> GetWeather(float timeout, CancellationToken cancellationToken)
    {
        var (result, latitude, longitude) = await _locationService.GetLocation(cancellationToken);
        
        return await GetWeather(latitude, longitude, timeout, cancellationToken);
    }
}