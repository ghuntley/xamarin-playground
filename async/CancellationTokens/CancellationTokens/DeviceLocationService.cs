using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationTokens
{
    public class DeviceLocationService : IDeviceLocationService
    {
        //private IGeolocatorService _geoLocatorService;
        //public DeviceLocationService(IGeolocatorService geolocatorService)
        //{
        //    _geoLocatorService = geolocatorService;
        //}

        public async Task UpdateDeviceLocationAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Cancellation has not been requested; sleeping for one second");

                var oneSecond = 1 * 1000;
                await Task.Delay(oneSecond);
			}

            Console.WriteLine("######################################");
            Console.WriteLine("# Parent has cancelled the operation #");
			Console.WriteLine("######################################");
		}
    }
}
