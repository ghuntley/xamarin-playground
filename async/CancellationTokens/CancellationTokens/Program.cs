using System.Threading;
using System.Threading.Tasks;

namespace CancellationTokens
{
    internal class MainClass
    {
        private static readonly IDeviceLocationService _deviceLocationService = new DeviceLocationService(new GeoLocatorService());
        private static readonly CancellationTokenSource _cancelationTokenSource = new CancellationTokenSource();

        public static void Main(string[] args)
        {
            var tenSeconds = 10 * 1000;
            _cancelationTokenSource.CancelAfter(tenSeconds);

            MainAsync(args).Wait();
        }

        private static async Task MainAsync(string[] args)
        {
            await _deviceLocationService.UpdateDeviceLocationAsync(_cancelationTokenSource.Token);
        }
    }
}