using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{

    //текущеий номер цвета инкрементируется полсе использования
    static public int currentColor = 0;



    //получить текущий цвет по значению
    public static Color GetNextColor()
    {
        Color c = Color.white;
        //назначаем переменной которую возвращаем новый цвет
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

    //Инкрементируем номер текущего цвета
    public static void IncrementColor()
    {
        currentColor++;

        //проверка на максимальное значение текущего цвета
        if (currentColor == 7)
            currentColor = 0;
    }

    //получить деволтный цвет блока ячеек поля
    public static Color GetDefaultColour()
    {
        Color c;
        ColorUtility.TryParseHtmlString("#353535", out c);
        return c;
    }
}
