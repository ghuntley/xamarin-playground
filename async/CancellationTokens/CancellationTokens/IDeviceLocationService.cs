using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationTokens
{
    public interface IDeviceLocationService
    {
		Task UpdateDeviceLocationAsync(CancellationToken cancellationToken = default(CancellationToken));
	}
}
