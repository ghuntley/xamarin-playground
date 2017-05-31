```shell
vbox86p:/ # am broadcast -a android.intent.action.BATTERY_LOW                                                                                                                                            
Broadcasting: Intent { act=android.intent.action.BATTERY_LOW }
Broadcast completed: result=0

vbox86p:/ # am broadcast -a android.intent.action.BOOT_COMPLETED                                                                                                                                         
Broadcasting: Intent { act=android.intent.action.BOOT_COMPLETED }
Broadcast completed: result=0
vbox86p:/ # 
```

```shell
$ adb -s 192.168.56.101:5555 logcat |egrep -i -e "DeviceLocation"
05-31 05:02:18.249 18828 18828 I UpdateDeviceLocationReceiver: Job has been scheduled to update the device location.
05-31 05:02:23.260 18828 18828 D UpdateDeviceLocationJobService: Starting '1'
05-31 05:02:23.288 18828 18828 D UpdateDeviceLocationJobService: '1' finished successfully
05-31 05:02:31.225 18828 18828 I UpdateDeviceLocationReceiver: Job has been scheduled to update the device location.
05-31 05:02:35.463 18828 18828 I UpdateDeviceLocationReceiver: Another device location update job will not run because an existing job was found in the queue.
05-31 05:02:36.200 18828 18828 I UpdateDeviceLocationReceiver: Another device location update job will not run because an existing job was found in the queue.
05-31 05:02:36.227 18828 18828 D UpdateDeviceLocationJobService: Starting '1'
05-31 05:02:36.227 18828 18828 D UpdateDeviceLocationJobService: '1' finished successfully
05-31 05:02:36.885 18828 18828 I UpdateDeviceLocationReceiver: Job has been scheduled to update the device location.
05-31 05:02:38.324 18828 18828 I UpdateDeviceLocationReceiver: Another device location update job will not run because an existing job was found in the queue.
05-31 05:02:39.265 18828 18828 I UpdateDeviceLocationReceiver: Another device location update job will not run because an existing job was found in the queue.
05-31 05:02:41.889 18828 18828 D UpdateDeviceLocationJobService: Starting '1'
05-31 05:02:41.889 18828 18828 D UpdateDeviceLocationJobService: '1' finished successfully
05-31 05:02:56.752 18828 18828 I UpdateDeviceLocationReceiver: Job has been scheduled to update the device location.
```
