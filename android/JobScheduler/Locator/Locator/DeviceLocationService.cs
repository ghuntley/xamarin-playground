using System;
using System.Threading;
using System.Threading.Tasks;
using Android.Util;

namespace Locator
{
    public interface IDeviceLocationService
    {
		Task UpdateDeviceLocation(CancellationToken cancellationToken = default(CancellationToken));
	}

    public class DeviceLocationService : IDeviceLocationService
    {
        private const string TAG = nameof(DeviceLocationService);

        public Task UpdateDeviceLocation(CancellationToken cancellationToken = default(CancellationToken))
        {
            Log.Info(TAG, "Updating device location");
            return Task.FromResult(true);
        }
    }
}
