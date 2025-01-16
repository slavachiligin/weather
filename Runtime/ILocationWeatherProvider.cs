using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface ILocationWeatherProvider
{
    Task<IReadOnlyList<string>> GetWeather(float timeout, CancellationToken cancellationToken);
}