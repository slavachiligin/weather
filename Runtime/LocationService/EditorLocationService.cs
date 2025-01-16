using System.Threading;
using System.Threading.Tasks;

namespace LocationService
{
    public class EditorLocationService : ILocationService
    {
        public Task<(bool, double, double)> GetLocation(CancellationToken cancellationToken)
        {
            return Task.FromResult((true, 55.796391, 49.108891));
        }
    }
}