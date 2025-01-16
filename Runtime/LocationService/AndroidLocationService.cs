using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Android;

namespace LocationService
{
    public class AndroidLocationService : LocationServiceBase
    {
        public override Task<(bool, double, double)> GetLocation(CancellationToken cancellationToken)
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermission(Permission.FineLocation);
            }

            if (!Input.location.isEnabledByUser)
            {
                Debug.LogError("Location services are not enabled on this device.");
                return Task.FromResult<(bool, double, double)>((false, 0, 0));
            }

            return base.GetLocation(cancellationToken);
        }
    }
}