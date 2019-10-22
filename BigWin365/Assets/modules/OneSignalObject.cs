using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OneSignalObject : MonoBehaviour
{
    [SerializeField] string id;

    void Start()
    {
        OneSignal.StartInit(id)
          .HandleNotificationOpened(HandleNotificationOpened)
          .EndInit();

        OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;

        OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.DEBUG, OneSignal.LOG_LEVEL.DEBUG);
    }

    private static void HandleNotificationOpened(OSNotificationOpenedResult result)
    {
        string url = result.notification.payload.launchURL;
        int index = url.IndexOf("app://com.news.testapk/");
        if (index >= 0)
        {
            url = url.Remove(index, "app://com.news.testapk/".Length);


            Debug.Log("---Onesignal  isOneSignalDeep = true; ");
            if (!openUrl("com.android.chrome", url))
            {
                if (!openUrl("com.opera.browser", url))
                {
                    if (!openUrl("org.mozilla.firefox", url))
                    {
                        if (!openUrl("com.yandex.browser", url))
                        {
                            if (!openUrl("com.UCMobile.intl", url))
                            {
                                if (!openUrl("com.gl9.cloudBrowser", url))
                                {
                                    Application.OpenURL(url);
                                }
                            }
                        }
                    }
                }
                else
                    Debug.Log("---Onesignal open opera url = " + url);
            }
            else
                Debug.Log("---Onesignal open chrome url = " + url);


        }

    }


    public static bool openUrl(string packageName, string url)
    {
#if UNITY_ANDROID
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject pManager = unityActivity.Call<AndroidJavaObject>("getPackageManager");

        //For accessing static strings(ACTION_VIEW) from android.content.Intent
        AndroidJavaClass intentStaticClass = new AndroidJavaClass("android.content.Intent");
        string actionView = intentStaticClass.GetStatic<string>("ACTION_VIEW");

        //Create Uri
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", url);

        //Psss ACTION_VIEW and Uri.parse to the intent
        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", actionView, uriObject);

        try
        {
            if (pManager.Call<AndroidJavaObject>("getPackageInfo", packageName, 0) != null)
            {
                intent.Call<AndroidJavaObject>("setPackage", packageName);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed to Open App 1: " + e.Message);
            return false;
        }

        try
        {
            unityActivity.Call("startActivity", intent);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed to Open App 2: " + e.Message);
            //Open with Browser
            string link = "https://play.google.com/store/apps/details?id=" + packageName + "&hl=en";

            // Application.OpenURL(link);///////////////////////////////
            return false;
        }
#endif
        return false;
    }

}
