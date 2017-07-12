﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace Locator
{
    [Activity(Label = "Locator", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private const string TAG = "BB-Locator";
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate { button.Text = $"{count++} clicks!"; };

            var receiver = new UpdateDeviceLocationReceiver();
            var filter = new IntentFilter();
            filter.AddAction(Intent.ActionScreenOff);
            filter.AddAction(Intent.ActionScreenOn);
            RegisterReceiver(receiver, filter);
        }
    }
}

