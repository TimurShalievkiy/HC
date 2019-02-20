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
                c =  Color.blue;
                break;
            case 1:
                c = Color.cyan;
                break;
            case 2:
               c = Color.gray;
                break;
            case 3:
                c = Color.green;
                break;
            case 4:
                c = Color.magenta;
                break;
            case 5:
                c = Color.red;
                break;
            case 6:
                c = Color.white;
                break;
            case 7:
                c = Color.yellow;
                break;
            case 8:
                c = new Color(0.2f,0.4f,05f);
                break;
         
        }
        return c;
    }
    public static void IncrementColor()
    {
        currentColor++;
        if (currentColor == 9)
            currentColor = 0;
    }
}
