using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    static public int currentColor = 0;
    public static Color GetNextColor()
    {
  

        Color c = Color.white;

        switch (currentColor)
        {                      
            case 0:
                ColorUtility.TryParseHtmlString("#4DD5AD", out c);
                break;
            case 1:
                ColorUtility.TryParseHtmlString("#5DBFE4", out c);
                break;
            case 2:
                ColorUtility.TryParseHtmlString("#FFC73C", out c);
                break;
            case 3:
                ColorUtility.TryParseHtmlString("#EF9549", out c);
                break;
            case 4:
                ColorUtility.TryParseHtmlString("#59CB86", out c);
                break;
            case 5:
                ColorUtility.TryParseHtmlString("#E86A82", out c);
                break;
            case 6:
                ColorUtility.TryParseHtmlString("#7989C4", out c);
                break;
         
        }
       
        return c;
    }
    public static void IncrementColor()
    {
        currentColor++;
        if (currentColor == 7)
            currentColor = 0;
    }
    public static Color GetDefaultColour()
    {
        Color c;
        ColorUtility.TryParseHtmlString("#353535", out c);
        return c;
    }
}
