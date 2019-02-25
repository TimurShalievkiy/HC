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
            item.transform.GetComponent<Image>().color = ColorManager.GetDefaultColour();
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
           // Debug.Log(targetIndex + " " + numBoxWithColl);
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


    public bool ChekShapeForPlacement(Transform parrent)
    {
        int length = BlockInShape.matrixLength;
        int numBoxWithColl = -1;
        int targetIndex = -1;

        List<int> listOfIndexs = new List<int>();

        for (int i = 0; i < parrent.childCount; i++)
        {
            if (parrent.GetChild(i).gameObject.activeSelf)
            {
                listOfIndexs.Add(parrent.GetChild(i).GetSiblingIndex());
            }
        }

        for (int i = 0; i < parrent.childCount; i++)
        {
            if (parrent.GetChild(i).GetComponent<BoxCollider2D>().isActiveAndEnabled)
            {
                numBoxWithColl = i;
                targetIndex = parrent.GetChild(i).GetComponent<BoxCollider2D>().GetComponent<BlockInShape>().TargetIndex;
            }
        }
        //
        //
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    //transform.GetChild(i).GetComponent<BlockInShape>().SetValueForCurrentTarger();
        //    if (transform.GetChild(i).gameObject.activeSelf)
        //    {
        //        if (transform.GetChild(i).gameObject.GetComponent<BoxCollider2D>().isActiveAndEnabled)
        //        {
        //            numBoxWithColl = transform.GetChild(i).GetSiblingIndex();

        //            targetIndex = transform.GetChild(i).GetComponent<BlockInShape>().TargetIndex;
        //        }
        //        listOfIndexs.Add(transform.GetChild(i).GetSiblingIndex());
        //    }
        //}

        if (targetIndex == -1 || numBoxWithColl == -1)
        {
            Debug.Log(targetIndex + " " + numBoxWithColl);
            return false;
        }
        else
            Debug.Log(targetIndex + " " + numBoxWithColl);

        ////int colPosX = numBoxWithColl / length;
        ////int colPosy = numBoxWithColl - colPosX * length;

        //////Debug.Log(colPosX + " +++ "+ colPosy);

        ////int targetPosX = targetIndex / 10;
        ////int targetPosY = targetIndex - targetPosX * 10;
        ////// Debug.Log(targetPosX + " --- " + targetPosY);
        ////Debug.Log(listOfIndex.Count);

        //int zeroPoint = targetIndex - numBoxWithColl - 10;

        //int x = zeroPoint + 10 * (int)(listOfIndexs[0] / BlockInShape.matrixLength) + listOfIndexs[0] % BlockInShape.matrixLength;


        //int line = (x / 10 - listOfIndexs[0] / 5);
        //for (int i = 0; i < listOfIndexs.Count; i++)
        //{
        //    x = zeroPoint + 10 * (int)(listOfIndexs[i] / BlockInShape.matrixLength) + listOfIndexs[i] % BlockInShape.matrixLength;
        //    if (x > 99 || x < 0)
        //        return false;

        //    if (field.GetChild(x).GetComponent<Cell>().isSet)
        //    {
        //        return false;
        //    }


        //    if (line != (x / 10 - listOfIndexs[i] / 5))
        //    {
        //        Debug.Log("linr = " + line + " != " + (x / 10 - listOfIndexs[i] / 5));
        //        return false;
        //    }

        //}
        //for (int i = 0; i < listOfIndexs.Count; i++)
        //{
        //    x = zeroPoint + 10 * (int)(listOfIndexs[i] / BlockInShape.matrixLength) + listOfIndexs[i] % BlockInShape.matrixLength;
        //    field.GetChild(x).GetComponent<Cell>().SetValue(true);
        //    field.GetChild(x).GetComponent<Image>().color = ColorManager.GetNextColor();

        //}

        string str = "";
        foreach (var item in listOfIndexs)
        {
            str += item + " ";
        }
        Debug.Log(str);
            return false;
    }
    public void MakeShapeShadowInGameField(Transform parrent)
    {
        if (!ChekShapeForPlacement(parrent))
        {
            return;
        }


    }
}
