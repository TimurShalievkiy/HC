using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldManager : MonoBehaviour
{
    public Transform field;
    

    public void CheckFieldForFullLines()
    {
        List<GameObject> listFullCells = new List<GameObject>();
        bool horizontal = true;
        bool vertical = true;

        for (int i = 0; i < 10; i++)
        {
            horizontal = true;
            vertical = true;

            for (int j = 0; j < 10; j++)
            {
                if (!field.GetChild(i * 10 + j).transform.GetComponent<Cell>().isSet)
                    horizontal = false;

                if (!field.GetChild(j * 10 + i).transform.GetComponent<Cell>().isSet)
                    vertical = false;
            }

            if (horizontal)
                foreach (GameObject item in ReturnListOfCellsByHorizontal(i))
                {
                    listFullCells.Add(item);
                }
            if (vertical)
                foreach (GameObject item in ReturnListOfCellsByVertical(i))
                {
                    listFullCells.Add(item);
                }
        }


        foreach (var item in listFullCells)
        {
            item.transform.GetComponent<Image>().color = ColorManager.SetDefaultColour();
            item.transform.GetComponent<Cell>().SetValue(false);
        }

    }

    List<GameObject> ReturnListOfCellsByHorizontal(int numHorizontal)
    {
        List<GameObject> listFullCells = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            listFullCells.Add(field.GetChild(numHorizontal * 10 + i).gameObject);
        }
        return listFullCells;
    }

    List<GameObject> ReturnListOfCellsByVertical(int numVertical)
    {
        List<GameObject> listFullCells = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            listFullCells.Add(field.GetChild(i * 10 + numVertical).gameObject);
        }
        return listFullCells;
    }

    public void  CheckShapeForPlacement(int targetIndex,int numBoxWithColl, List<int> listOfIndex)
    {
        int length = BlockInShape.matrixLength;

        if (targetIndex == -1 || numBoxWithColl == -1)
        {
            Debug.Log(targetIndex + " " + numBoxWithColl);
            return;
        }

        //int colPosX = numBoxWithColl / length;
        //int colPosy = numBoxWithColl - colPosX * length;

        ////Debug.Log(colPosX + " +++ "+ colPosy);

        //int targetPosX = targetIndex / 10;
        //int targetPosY = targetIndex - targetPosX * 10;
        //// Debug.Log(targetPosX + " --- " + targetPosY);
        //Debug.Log(listOfIndex.Count);

        int zeroPoint = targetIndex - numBoxWithColl -10;

        int x = zeroPoint + 10 * (int)(listOfIndex[0] / BlockInShape.matrixLength) + listOfIndex[0] % BlockInShape.matrixLength;


        int line = (x / 10 - listOfIndex[0] / 5);
        for (int i = 0; i < listOfIndex.Count; i++)
        {
            x = zeroPoint + 10*(int)(listOfIndex[i] / BlockInShape.matrixLength) + listOfIndex[i] % BlockInShape.matrixLength;
            if(x>99||x<0)
                return;

            if (field.GetChild(x).GetComponent<Cell>().isSet)
            {
                return;
            }


            Debug.Log(x);
            Debug.Log((x / 10) + "-" + (listOfIndex[i] / 5) + " = " + (x / 10 - listOfIndex[i] / 5));
            if (line != (x / 10 - listOfIndex[i] / 5))
            {
                Debug.Log("linr = " + line + " != " + (x / 10 - listOfIndex[i] / 5));
                return;
            }        

        }
        for (int i = 0; i < listOfIndex.Count; i++)
        {
            x = zeroPoint + 10 * (int)(listOfIndex[i] / BlockInShape.matrixLength) + listOfIndex[i] % BlockInShape.matrixLength;
            field.GetChild(x).GetComponent<Cell>().SetValue(true);
            field.GetChild(x).GetComponent<Image>().color = ColorManager.GetNextColor();

        }
        ColorManager.IncrementColor();
    }
}
