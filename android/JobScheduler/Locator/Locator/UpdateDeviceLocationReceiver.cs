using Android.App;
using Android.Content;
using Android.App.Job;
using System.Linq;
using Android.Util;

namespace Locator
{
	[BroadcastReceiver(Enabled = true, Exported = true, Label = "Update Device Location")]
	[IntentFilter(new[] { Intent.ActionBootCompleted, Intent.ActionUserPresent, Intent.ActionBatteryLow, Intent.ActionPowerConnected, Intent.ActionPowerDisconnected})]
	public class UpdateDeviceLocationReceiver : BroadcastReceiver
	{
		private const int RUN_IMMEDIATELY = 1;
		private const int RUN_DURING_NEXT_WINDOW = 2;
        private const string TAG = "BB-Locator";

		private void RunImmediately(Context context, Intent intent)
		{
			JobScheduler jobScheduler = (JobScheduler)context.GetSystemService(Context.JobSchedulerService);

            // don't queue another job if there's already one in the pending pjob queue
			if (jobScheduler.AllPendingJobs.Any(x => x.Id == RUN_IMMEDIATELY))
			{
				Log.Info(TAG, "Another device location update job will not run because an existing job was found in the queue.");

				return;
			}

			var job = new JobInfo.Builder(RUN_IMMEDIATELY, new ComponentName(context, Java.Lang.Class.FromType(typeof(UpdateDeviceLocationJobService))));

			job.SetMinimumLatency(100);
			job.SetTriggerContentMaxDelay(1000);

			var result = jobScheduler.Schedule(job.Build());

            LogScheduleResult(result);
		}


		private void RunDuringNextWindowIfNotAlreadyScheduled(Context context, Intent intent)
		{
			JobScheduler jobScheduler = (JobScheduler)context.GetSystemService(Context.JobSchedulerService);

			// don't queue another job if there's already one in the pending job queue
			if (jobScheduler.AllPendingJobs.Any(x => x.Id == RUN_DURING_NEXT_WINDOW))
			{
				Log.Info(TAG, "Another device location update job will not be scheduled because an existing job was found in the queue.");

				return;
			}

			var job = new JobInfo.Builder(RUN_DURING_NEXT_WINDOW, new ComponentName(context, Java.Lang.Class.FromType(typeof(UpdateDeviceLocationJobService))));

            var oneMinute = 1 * 60 * 1000;
            var twoMinutes = 2 * 60 * 1000;
            var fiveMinutes = 5 * 60 * 1000;

            job.SetMinimumLatency(oneMinute);            // Specify that this job should be delayed by the provided amount of time (in milliseconds)
            job.SetTriggerContentMaxDelay(twoMinutes);   // Set the maximum total delay (in milliseconds) that is allowed
            //job.SetPeriodic(fiveMinutes);              // Specify that this job should recur with the provided interval, not more than once per period. 
            //job.SetPersisted(true);

            var result = jobScheduler.Schedule(job.Build());

            LogScheduleResult(result);
		}

		private void LogScheduleResult(int scheduleResult)
		{
			if (scheduleResult == JobScheduler.ResultSuccess)
			{
				Log.Info(TAG, "Job has been scheduled to update the device location.");
			}
			else
			{
				Log.Error(TAG, "Failed to schedule a job to update the device location.");
			}
		}

		public override void OnReceive(Context context, Intent intent)
		{
            Log.Debug(TAG, "BroadcastReceiver received " + intent);

            switch (intent.Action)
			{
				case Intent.ActionBatteryLow:
				case Intent.ActionPowerConnected:
				case Intent.ActionPowerDisconnected:
					RunImmediately(context, intent);
					break;

				case Intent.ActionTimeTick:
				case Intent.ActionBootCompleted:
				case Intent.ActionUserPresent:
				case Intent.ActionScreenOff:
				case Intent.ActionScreenOn:
					RunDuringNextWindowIfNotAlreadyScheduled(context, intent);
					break;

				default:
					RunDuringNextWindowIfNotAlreadyScheduled(context, intent);
					break;
			}
		}
	}
}
