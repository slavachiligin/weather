using System.Threading;
using LocationService;
using Samples.Scripts;
using UnityEngine;
using WeatherServices;

public class Example : MonoBehaviour
{
    private void Start()
    {
        StartWeatherServiceExample();
        StartLocationWeatherServiceExample();
    }

    private static async void StartWeatherServiceExample()
    {
        var weatherServiceProvider = new WeatherServiceProvider();

        weatherServiceProvider.AddService(new OpenMeteoWeatherService());
        weatherServiceProvider.AddService(new OpenWeatherMapWeatherService());

        using var cts = new CancellationTokenSource();

        var result = await weatherServiceProvider.GetWeather(ExampleConstants.Latitude, ExampleConstants.Longitude,
            ExampleConstants.Timeout, cts.Token);

        foreach (var weatherResult in result)
        {
            Debug.Log(weatherResult);
        }
    }

    private static async void StartLocationWeatherServiceExample()
    {
        var locationWeatherServiceProvider = new LocationWeatherServiceProvider(LocationServiceProvider.GetLocationService());

        locationWeatherServiceProvider.AddService(new OpenMeteoWeatherService());
        locationWeatherServiceProvider.AddService(new OpenWeatherMapWeatherService());

        using var cts = new CancellationTokenSource();

        var result = await locationWeatherServiceProvider.GetWeather(ExampleConstants.Latitude, ExampleConstants.Longitude,
            ExampleConstants.Timeout, cts.Token);
        var resultWithoutLocation = await locationWeatherServiceProvider.GetWeather(ExampleConstants.Timeout, cts.Token);
        
        foreach (var weatherResult in result)
        {
            Debug.Log(weatherResult);
        }
        
        foreach (var weatherResult in resultWithoutLocation)
        {
            Debug.Log(weatherResult);
        }
    }
}