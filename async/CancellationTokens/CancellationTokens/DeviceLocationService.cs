using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationTokens
{
    public class DeviceLocationService : IDeviceLocationService
    {
        private IGeoLocatorService _geoLocatorService;

        public DeviceLocationService(IGeoLocatorService geolocatorService)
        {
            _geoLocatorService = geolocatorService;
        }

        //public async Task UpdateDeviceLocationAsync(CancellationToken cancellationToken)
        //{
        //    var i = 0;

        //    while (i < 10 && !cancellationToken.IsCancellationRequested)
        //    {
        //        Console.WriteLine($"Cancellation has not been requested after '{i}' seconds");

        //        var oneSecond = 1 * 1000;

        //        await Task.Delay(oneSecond);
        //        i++;
        //    }
        //    if (cancellationToken.IsCancellationRequested)
        //        return;

        //    Console.WriteLine("Been running for too long bailing");
        //    cancellationToken.ThrowIfCancellationRequested();
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