using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScinController : MonoBehaviour
{
    public static string currentScin;

    public void SetScin(string s)
    {
        PlayerPrefs.SetString("currentScin", s);
        currentScin = s;
    }
    public static string GetScin()
    {

        if (PlayerPrefs.HasKey("currentScin"))
        {
            currentScin = PlayerPrefs.GetString("currentScin");
        }
        else
            currentScin = "FreeBuilding";
        return currentScin;
    }
}
