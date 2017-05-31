using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace OnBoot
{
    [BroadcastReceiver(Permission = "android.permission.RECEIVE_BOOT_COMPLETED")]
	[IntentFilter(new string[] { Intent.ActionBootCompleted })]
	public class OnBootReceiver : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			Log.Debug("OnBootReceiver", "Hi, Mom!");
		}
	}
}