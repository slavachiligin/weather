using System.Threading;
using System.Threading.Tasks;

namespace LocationService
{
    public interface ILocationService
    {
        Task<(bool, double, double)> GetLocation(CancellationToken cancellationToken); 
    }
}