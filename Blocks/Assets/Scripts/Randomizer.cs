using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    public static float[,] chanses = {
    {   0   ,   6   },
    {   1   ,   6   },
    {   2   ,   1   },
    {   3   ,   6   },
    {   4   ,   6   },
    {   5   ,   5   },
    {   6   ,   4.5f },
    {   7   ,   6   },
    {   8   ,   6   },
    {   9   ,   5   },
    {   10  ,   4.5f },
    {   11  ,   7   },
    {   12  ,   7   },
    {   13  ,   7   },
    {   14  ,   7   },
    {   15  ,   4   },
    {   16  ,   4   },
    {   17  ,   4   },
    {   18  ,   4   },

    };

    public static int GetNextShapeId()
    {
        List<ShapeInRandom> listShapesInRandom = new List<ShapeInRandom>();
        float chance = 100f / (ShapesManager.GetAllShapes().Count);

        int index = 0;
        foreach (var item in ShapesManager.GetAllShapes())
        {
            listShapesInRandom.Add(new ShapeInRandom(index,chance));
            index++;
        }

        float value = Random.Range(0f,1001f);
        float currentSumm = 0;
        int id = -1;


        float minVal = 0;
        for (int i = 0; i < listShapesInRandom.Count; i++)
        {
            minVal = currentSumm;
            currentSumm += listShapesInRandom[i].GetValueByPercent();
            if (minVal <= value && value < currentSumm)
            {

                id = listShapesInRandom[i].id;
            }
            //Debug.Log(minVal + " " + value + " " + currentSumm);
        }
       


       // Debug.Log(value);
        return id;
    }

    

}


class ShapeInRandom
{
    public float chance;
    public int id;

    public ShapeInRandom(int id,float chance)
    {
        this.chance = chance;
        this.id = id;
    }

    public float GetValueByPercent()
    {

        return 10f * Randomizer.chanses[id,1];
    }
}
