using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldManager : MonoBehaviour
{
    public static Transform field;
    public Transform touchZonesParent;
    public GameObject RevivePanel;
    public ScoreManager scoreManager;

    public void Start()
    {
        field = this.transform;
        ResetGameField();
    }
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
                    if (listFullCells.IndexOf(item) == -1)
                        listFullCells.Add(item);
                }


            if (vertical)
                foreach (GameObject item in ReturnListOfCellsByVertical(i))
                {
                    if (listFullCells.IndexOf(item) == -1)
                        listFullCells.Add(item);
                }
        }

        foreach (var item in listFullCells)
        {
            item.transform.GetComponent<Image>().color = ColorManager.GetDefaultColour();
            item.transform.GetComponent<Cell>().SetValue(false);
        }
        scoreManager.IncreaseScore(listFullCells.Count);
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




    public bool ChekShapeForPlacement(Transform touchZone)
    {
        CleerFieldColor();
        int length = BlockInShape.matrixLength;
        int numBoxWithColl = -1;
        int targetIndex = -1;
        Color color = new Color();

        List<int> listOfIndexs = new List<int>();

        for (int i = 0; i < touchZone.childCount; i++)
        {
            if (touchZone.GetChild(i).GetComponent<Image>().enabled)
            {
                listOfIndexs.Add(touchZone.GetChild(i).GetSiblingIndex());
            }
        }

        for (int i = 0; i < touchZone.childCount; i++)
        {
            if (touchZone.GetChild(i).GetComponent<BoxCollider2D>().isActiveAndEnabled)
            {
                numBoxWithColl = i;
                targetIndex = touchZone.GetChild(i).GetComponent<BoxCollider2D>().GetComponent<BlockInShape>().TargetIndex;
                color = touchZone.GetComponent<TouchZone>().currentColor;
                break;
            }
        }

        if (targetIndex == -1 || numBoxWithColl == -1)
        {
            //Debug.Log(targetIndex + " " + numBoxWithColl);
            return false; 
        }

        int zeroPoint = targetIndex - numBoxWithColl - (BlockInShape.matrixLength * (int)(numBoxWithColl / BlockInShape.matrixLength));
       // Debug.Log(zeroPoint);

        int x = zeroPoint + 10 * (int)(listOfIndexs[0] / BlockInShape.matrixLength) + listOfIndexs[0] % BlockInShape.matrixLength;

       // Debug.Log(x);
        int line = (x / 10 - listOfIndexs[0] / 5);


        for (int i = 0; i < listOfIndexs.Count; i++)
        {
            x = zeroPoint + 10 * (int)(listOfIndexs[i] / BlockInShape.matrixLength) + listOfIndexs[i] % BlockInShape.matrixLength;
            if (x > 99 || x < 0)
                return false;

            if (field.GetChild(x).GetComponent<Cell>().isSet)
            {
                return false;
            }


            if (line != (x / 10 - listOfIndexs[i] / 5))
            {
                Debug.Log("linr = " + line + " != " + (x / 10 - listOfIndexs[i] / 5));
                return false;
            }

        }
        for (int i = 0; i < listOfIndexs.Count; i++)
        {
            x = zeroPoint + 10 * (int)(listOfIndexs[i] / BlockInShape.matrixLength) + listOfIndexs[i] % BlockInShape.matrixLength;
            field.GetChild(x).GetComponent<Cell>().SetValue(true);
            field.GetChild(x).GetComponent<Image>().color = color;

        }
        
        //Debug.Log(listOfIndexs.Count);
        scoreManager.IncreaseScore(listOfIndexs.Count);
        return true;
    }


    public void CleerFieldColor()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).GetComponent<Cell>().isSet)
            {
                transform.GetChild(i).GetComponent<Image>().color = ColorManager.GetDefaultColour();
            }
        }
    }

    public bool CheckThePossibilityOfPlacement(Transform touchZone)
    {
        int counter = 0;
        int numBoxWithColl = -1;

        List<int> listOfIndexs = new List<int>();

        for (int i = 0; i < touchZone.childCount; i++)
        {
            if (touchZone.GetChild(i).GetComponent<Image>().enabled)
            {
                listOfIndexs.Add(touchZone.GetChild(i).GetSiblingIndex());
            }
        }

        for (int i = 0; i < touchZone.childCount; i++)
        {
            if (touchZone.GetChild(i).GetComponent<BoxCollider2D>().isActiveAndEnabled)
            {
                numBoxWithColl = i;
                break;
            }
        }

        if (numBoxWithColl == -1)
        {           
            return false;
        }

        int zeroPoint = -1;
        bool flag = true;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            flag = true;

            if (transform.GetChild(i).GetComponent<Cell>().isSet)
                continue;

            zeroPoint = i - numBoxWithColl - (BlockInShape.matrixLength * (int)(numBoxWithColl / BlockInShape.matrixLength));
            
            int x = zeroPoint + 10 * (int)(listOfIndexs[0] / BlockInShape.matrixLength) + listOfIndexs[0] % BlockInShape.matrixLength;
            //Debug.Log(x);
            int line = (x / 10 - listOfIndexs[0] / 5);


            for (int j = 0; j < listOfIndexs.Count; j++)
            {
                x = zeroPoint + 10 * (int)(listOfIndexs[j] / BlockInShape.matrixLength) + listOfIndexs[j] % BlockInShape.matrixLength;
                if (x > 99 || x < 0)
                {
                    flag = false;
                    break;
                }

                if (field.GetChild(x).GetComponent<Cell>().isSet)
                {
                    flag = false;
                    break;
                }


                if (line != (x / 10 - listOfIndexs[j] / 5))
                {
                    flag = false;
                    break;
                }
                

            }
            if (flag)
            {
                counter++;
            }
            
        }
        //Debug.Log("============================");
        if (counter == 0)
            return false;

        return true;
    }
    public void CheckForLoss()
    {
        int count = 0;
        if (touchZonesParent.childCount > 1)
        {
            for (int i = 0; i < touchZonesParent.childCount; i++)
            {
                if (CheckThePossibilityOfPlacement(touchZonesParent.GetChild(i)))
                {
                    count++;
                }
            }
            if (count == 0)
            {
                RevivePanel.GetComponent<TimerForRevive>().ResetTimer();
                RevivePanel.SetActive(true);
            }
        }
    }

    public void ResetGameField()
    {
        for (int i = 0; i < field.childCount; i++)
        {
            field.GetChild(i).GetComponent<Cell>().isSet = false;
        }
        CleerFieldColor();
        scoreManager.ResetScore();
        TouchZonesCreator.DestroyAllZones(touchZonesParent);

    }
}

