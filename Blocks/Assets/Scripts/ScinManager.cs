using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScinManager : MonoBehaviour
{
    public static string currentSkin ;
    static int indexOfCurrentSq = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("currentSkin"))
        {
            currentSkin = PlayerPrefs.GetString("currentSkin");
        }
        else
        {
            currentSkin = "default";
        }
    }


    public static Sprite GetBackground()
    {
        if (PlayerPrefs.HasKey("currentSkin"))
        {
            currentSkin = PlayerPrefs.GetString("currentSkin");
        }
        else
        {
            currentSkin = "default";
        }

        Sprite s = Resources.Load<Sprite>("Skins/" + currentSkin + "/bg/bg");

        //if (s == null)
        //{
        //    s = Resources.Load<Sprite>("Skins/default/bg/bg");
        //    Debug.Log("null = " + s);
        //    return s;
        //}
        //Debug.Log("no null = " + s.name);

        return s;

    }
    public static Sprite GetNextSq()
    {
        if (PlayerPrefs.HasKey("currentSkin"))
        {
            currentSkin = PlayerPrefs.GetString("currentSkin");
        }
        else
        {
            currentSkin = "default";
        }

        Sprite[] ls = Resources.LoadAll<Sprite>("Skins/" + currentSkin + "/sq");

        if (indexOfCurrentSq > ls.Length-1)
        {
            indexOfCurrentSq = 0;
        }
        return ls[indexOfCurrentSq];
    }

    public static Sprite GetCell()
    {
        Sprite s = Resources.Load<Sprite>("Skins/" + currentSkin + "/cell/cell");
        

        return s;
    }
    public static void IncrementIndexOfCurrentSq()
    {
        indexOfCurrentSq++;
    }
    public void SetCurrentScinValue(string s)
    {
        currentSkin = s;
        PlayerPrefs.SetString("currentSkin", currentSkin);
    }

}
