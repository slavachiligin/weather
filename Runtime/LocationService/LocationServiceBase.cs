using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace LocationService
{
    public abstract class LocationServiceBase : ILocationService
    {
        public virtual async Task<(bool, double, double)> GetLocation(CancellationToken cancellationToken)
        {
            if (Input.location.status == LocationServiceStatus.Stopped)
            {
                Input.location.Start();
            }
            
            while (Input.location.status == LocationServiceStatus.Initializing)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Input.location.Stop();
                    throw new OperationCanceledException(cancellationToken);
                }

                await Task.Yield();
            }

            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Input.location.Stop();
                
                Debug.LogError("Location service is failed");
                return (false, 0, 0);
            }

            if (!Input.location.isEnabledByUser)
            {
                Debug.LogError("Location services are not enabled on this device.");
                return (false, 0, 0);
            }

            var locationData = Input.location.lastData;
            var latitude = locationData.latitude;
            var longitude = locationData.longitude;

            Input.location.Stop();

            return (true, latitude, longitude);
        }
    }
}