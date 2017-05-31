using Android.App;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;
using System;
using Android.App.Job;
using System.Collections.Generic;
using System.Threading;
using Android.Util;

namespace Locator
{
	[Service(Enabled = true, Exported = true, Permission = "android.permission.BIND_JOB_SERVICE")]
	public class UpdateDeviceLocationJobService : JobService
	{
        private const string TAG = nameof(UpdateDeviceLocationJobService);
        private readonly IDeviceLocationService _deviceLocationService = new DeviceLocationService();

		private static Dictionary<int, CancellationTokenSource> _cancellationTokens;

        public UpdateDeviceLocationJobService()
        {
			_cancellationTokens = new Dictionary<int, CancellationTokenSource>();
		}
		public override bool OnStartJob(JobParameters @params)
		{
			var jobIdentifier = @params.JobId;

            Log.Debug(TAG, $"Starting Job '{jobIdentifier}'");

			try
			{
                _cancellationTokens.Add(jobIdentifier, new CancellationTokenSource());

				var cancelationToken = _cancellationTokens[jobIdentifier].Token;

				// This service executes each incoming job on a Handler running on your application's main thread. 
				// This means that you must offload your execution logic to another thread/handler/AsyncTask`3 of your choosing. 
				// Not doing so will result in blocking any future callbacks from the JobManager
				// specifically JobService.OnStopJob(JobParameters), which is meant to inform you that the
				// scheduling requirements are no longer being met.
				//
				// See  https://developer.xamarin.com/api/type/Android.App.Job.JobService/
				Task.Run(async () => {
                    try {
                        await _deviceLocationService.UpdateDeviceLocation(cancelationToken);
					}
                    catch (Exception ex)
                    {
                        Log.Error(TAG, $"Job '{jobIdentifier}' threw an exception: {ex}");
                    }
                });

                Log.Debug(TAG, $"Finished processing Job'{jobIdentifier}'");

			}
			catch (Exception ex)
			{
				Log.Error(TAG, $"Job '{jobIdentifier}' threw an exception: {ex}");
			}
			finally
			{
                _cancellationTokens.Remove(jobIdentifier);
				JobFinished(@params, needsReschedule: false);
			}

			// True if your service needs to process the work (on a separate thread). False if there's no more work to be done for this job.
			//
			// See https://developer.android.com/reference/android/app/job/JobService.html
			return true;
		}

		public override bool OnStopJob(JobParameters @params)
		{
			Log.Debug(TAG, $"Operating system requested '{@params.JobId}' to stop");

			_cancellationTokens[@params.JobId].Cancel();

			Log.Debug(TAG, $"'{@params.JobId}' has stopped");

			// True to indicate to the JobManager whether you'd like to reschedule this job based on the retry criteria provided at job creation-time. 
			// False to drop the job. Regardless of the value returned, your job must stop executing.
			//
			// See https://developer.android.com/reference/android/app/job/JobService.html

			return true;
		}
	}
}