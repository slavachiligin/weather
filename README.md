# Readme

1. [Weather Service Integration Package for Unity](#weather-service-integration-package-for-unity)
2. [Features](#features)
3. [Installation](#installation)
4. [Quick Start](#quick-start)
5. [Example Usage](#example-usage)
6. [Adding Custom Weather Services](#adding-custom-weather-services)
7. [API Documentation](#api-documentation)
8. [Location Services Integration](#location-services-integration)
   - [Android Setup](#android-setup)
   - [iOS Setup](#ios-setup)

---

# Weather Service Integration Package for Unity

This Unity package provides a framework for integrating weather services into your Unity project. It supports multiple weather APIs, offers methods for fetching weather data by geographic coordinates, and is extensible for adding custom services. The package is designed to be lightweight, easy to use, and flexible.

---

## Features

- **Weather Service Management**: Easily add and manage multiple weather services.
- **Integrated APIs**: Includes support for:
  - [Open-Meteo](https://open-meteo.com/)
  - [OpenWeatherMap](https://openweathermap.org/api)
- **Weather Data Retrieval**: Fetch weather data by specifying latitude and longitude, with support for:
  - Timeout settings
  - Request cancellation via `CancellationToken`
- **Extensibility**: Add your own weather service integrations.

---

## Installation

1. Open your Unity project.
2. Navigate to **Window > Package Manager**.
3. Click the **+** button and select **Add package from git URL...**.
4. Paste the following URL:

   ```plaintext
   https://github.com/your-repo/weather-service-package.git
 5. Click Add to install the package.
 
 # Quick Start
 
 ### Example Usage
 
 Below is an example of how to use the Weather Service Integration package to fetch weather data using the provided services.
 
 ```csharp
 using System.Threading;
 using LocationService;
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
         
         // Adding integrated services
         weatherServiceProvider.AddService(new OpenMeteoWeatherService());
         weatherServiceProvider.AddService(new OpenWeatherMapWeatherService());
         
         using var cts = new CancellationTokenSource();
 
         // Request weather data for specific coordinates
         var result = await weatherServiceProvider.GetWeather(37.7749, -122.4194, 5000, cts.Token);
 
         foreach (var weatherResult in result)
         {
             Debug.Log(weatherResult);
         }
     }
 
     private static async void StartLocationWeatherServiceExample()
     {
         var locationWeatherServiceProvider = new LocationWeatherServiceProvider(LocationServiceProvider.GetLocationService());
         
         // Adding integrated services
         locationWeatherServiceProvider.AddService(new OpenMeteoWeatherService());
         locationWeatherServiceProvider.AddService(new OpenWeatherMapWeatherService());
         
         using var cts = new CancellationTokenSource();
 
         // Request weather data using location services
         var result = await locationWeatherServiceProvider.GetWeather(5000, cts.Token);
 
         foreach (var weatherResult in result)
         {
             Debug.Log(weatherResult);
         }
     }
 }
```

## Adding Custom Weather Services

The package is designed to be extensible. To add your own weather service, implement the `IWeatherService` interface.

### Example: Custom Weather Service

```csharp
using WeatherServices;

public class MyCustomWeatherService : IWeatherService
{
    private static string BaseUrl => "https://api.mycustomweather.com/v1/weather";

    public string BuildUrl(double latitude, double longitude)
    {
        return $"{BaseUrl}?lat={latitude:F2}&lon={longitude:F2}&data=temperature";
    }
}
```
### Steps to Add Custom Service

1. **Create a class** that implements the `IWeatherService` interface.
2. **Override the `BuildUrl` method** to construct the appropriate request URL.
3. **Add your custom service to the provider**:

```csharp
var weatherServiceProvider = new WeatherServiceProvider();
weatherServiceProvider.AddService(new MyCustomWeatherService());
```

## API Documentation

### WeatherServiceProvider

- **`AddService(IWeatherService service)`**  
  Adds a weather service to the provider.

- **`GetWeather(double latitude, double longitude, int timeout, CancellationToken cancellationToken)`**  
  Fetches weather data from all added services for the given coordinates.

---

### LocationWeatherServiceProvider

- **`GetWeather(int timeout, CancellationToken cancellationToken)`**  
  Fetches weather data using the device's current location.

---

## Location Services Integration

This package supports using Unity's Location Service to retrieve the device's current location and fetch weather data based on those coordinates.

To use Location Service on Android and iOS devices, some additional setup is required:

### Android
1. Open your **AndroidManifest.xml** file located in `Assets/Plugins/Android/AndroidManifest.xml`.
2. Add the following permissions to allow location access:

   ```xml
   <uses-permission android:name="android.permission.ACCESS_FINE_LOCATION"/>
   <uses-permission android:name="android.permission.ACCESS_COARSE_LOCATION"/>
   ```
   
3. Ensure the following permissions request is added within the `<application>` tag in your **AndroidManifest.xml** file:
   
  ```xml
     <meta-data android:name="com.google.android.gms.version" android:value="@integer/google_play_services_version"/>
   ```

### iOS
1. Open the **Info.plist** file located in `Assets/Plugins/iOS/Info.plist`.
2. Add the following keys to request location services permission from the user:

```xml
<key>NSLocationWhenInUseUsageDescription</key>
<string>We need your location to provide weather data based on your current position.</string>
<key>NSLocationAlwaysUsageDescription</key>
<string>We need your location to provide weather data based on your current position.</string>