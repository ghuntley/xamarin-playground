using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationTokens
{
    class MainClass
    {
        private static IDeviceLocationService _deviceLocationService = new DeviceLocationService();
        private static CancellationTokenSource _cancelationTokenSource = new CancellationTokenSource();

        public static void Main(string[] args)
        {
            var tenSeconds = 10 * 1000;
            _cancelationTokenSource.CancelAfter(tenSeconds);

            MainAsync(args).Wait();
		}

		static async Task MainAsync(string[] args)
		{
            await _deviceLocationService.UpdateDeviceLocationAsync(_cancelationTokenSource.Token);
		}
    }
}
