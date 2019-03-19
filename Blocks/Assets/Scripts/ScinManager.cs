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

        //если значение скина не было сохранено назначаем базовый дефолтный
        if (PlayerPrefs.HasKey("currentSkin"))
        {
            currentSkin = PlayerPrefs.GetString("currentSkin");
        }
        else
        {
            currentSkin = "default";
        }
    }

    //получение бекграунда текущего скина
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

        Sprite s = Resources.LoadAll<Sprite>("Skins/" + currentSkin + "/bg")[0];

        return s;

    }

    //Получение блоков в зависимости от текущего значение индекса блока и названия скина
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

    //получение ячейки с папки текущего скина
    public static Sprite GetCell()
    {
        Sprite s = Resources.LoadAll<Sprite>("Skins/" + currentSkin + "/cell")[0];
        return s;
    }

    //увеличение значение текущего блока
    public static void IncrementIndexOfCurrentSq()
    {
        indexOfCurrentSq++;
    }

    //задание значение текущего скина
    public void SetCurrentScinValue(string s)
    {
        currentSkin = s;
        PlayerPrefs.SetString("currentSkin", currentSkin);
    }

}
