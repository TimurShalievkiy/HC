using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSignalObject : MonoBehaviour
{

    void Start()
    { 
        OneSignal.StartInit("6f7f302e-3cf4-4c0c-ab78-57433f539b72")
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
