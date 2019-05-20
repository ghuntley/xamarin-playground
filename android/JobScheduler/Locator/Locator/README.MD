## **Creating and debugging Android background services with JobScheduler and adb**

**Learning Goals**

1. Android application life cycle
2. Awareness of JobScheduler and how it works
3. Getting people comfortable with Android Debug Bridge (ADB)
4. Awareness of how to troubleshoot problems with services


**Talking points**

* Application life-cycle of Android
* JobScheduler
* Broadcast Receiver
* Debugging background services
* JobService


**Useful ADB Commands**

```
adb devices		Print a list of all connected devices
adb logcat		View the device log
adb push		Upload a file to the device
adb pull		Download a file from the device
adb shell		shell on the local device
	
logcat is very helpful but the signal to noise is absurd. Make your life easier by giving your all your log statements a common tag and then you can filter the log with the following command:

		'adb logcat | findstr TAG'

-s can be used for all adb commands and will allow you to connect to a specific device using the serial if more than one device is connected.

		'adb -s ZY3223W6B4 logcat'

```


**Triggering manual broadcasts**

```
$ adb shell

vbox86p:/ # am broadcast -a android.intent.action.BATTERY_LOW                                                                                                                                            
Broadcasting: Intent { act=android.intent.action.BATTERY_LOW }
Broadcast completed: result=0

vbox86p:/ # am broadcast -a android.intent.action.BOOT_COMPLETED                                                                                                                                         
Broadcasting: Intent { act=android.intent.action.BOOT_COMPLETED }
Broadcast completed: result=0
vbox86p:/ # 

```

**Sample logcat output**

```
$ adb logcat | findstr BB-Locator
07-12 11:12:54.247  4319  4319 D BB-Locator: BroadcastReceiver received Intent { act=android.intent.action.SCREEN_ON flg=0x50000010 }
07-12 11:12:54.310  4319  4319 I BB-Locator: Job has been scheduled to update the device location.
07-12 11:12:54.331  4319  4319 D BB-Locator: BroadcastReceiver received Intent { act=android.intent.action.SCREEN_OFF flg=0x50000010 }
07-12 11:12:54.341  4319  4319 I BB-Locator: Another device location update job will not be scheduled because an existing job was found in the queue.
07-12 11:12:55.902  4319  4319 D BB-Locator: BroadcastReceiver received Intent { act=android.intent.action.USER_PRESENT flg=0x24000010 cmp=com.ghuntley.playground.locator/md51594a4bf5289234238ccd9d6873907ae.UpdateDeviceLocationReceiver }
07-12 11:12:55.907  4319  4319 I BB-Locator: Another device location update job will not be scheduled because an existing job was found in the queue.
07-12 11:12:55.961  4319  4319 D BB-Locator: BroadcastReceiver received Intent { act=android.intent.action.SCREEN_ON flg=0x50000010 }
07-12 11:12:55.962  4319  4319 I BB-Locator: Another device location update job will not be scheduled because an existing job was found in the queue.
```



### **Reference links:**

**JobScheduler** - https://developer.android.com/reference/android/app/job/JobScheduler.html

**JobInfo.Builder** - https://developer.android.com/reference/android/app/job/JobInfo.Builder.html - setPeriodic, SetMinimumLatency etc

**Android Intents** - https://developer.android.com/reference/android/content/Intent.html - System Broadcasts are listed here, check CONSTANTS for 'Broadcast action'

**BroadcastReceiver** - https://developer.android.com/guide/components/broadcasts.html

**Android Debug Bridge (ADB)** - https://developer.android.com/studio/command-line/adb.html