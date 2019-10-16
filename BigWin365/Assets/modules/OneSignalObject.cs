using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSignalObject : MonoBehaviour
{

    void Start()
    { 
        OneSignal.StartInit("e48420e9-1881-41c6-be6c-4311583576cd")
          .HandleNotificationOpened(HandleNotificationOpened)
          .EndInit();

        OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;

        OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.DEBUG, OneSignal.LOG_LEVEL.DEBUG);
    }

    private static void HandleNotificationOpened(OSNotificationOpenedResult result)
    {
        // use notification opened result here
    }
}
