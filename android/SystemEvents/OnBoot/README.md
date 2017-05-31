# Testing

Retrieve listing of devices
```shell
# adb devices
```

In terminal #1
```shell
# adb shell -s $deviceId
# am broadcast -a android.intent.action.BOOT_COMPLETED                                                                                                                                         
Broadcasting: Intent { act=android.intent.action.BOOT_COMPLETED }
Broadcast completed: result=0
```

In terminal #2
```shell
# adb logcat -s $deviceId
```

# Remarks
* `BOOT_COMPLETED` can only be send to a rooted device or an emulator from shell 
