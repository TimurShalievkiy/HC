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
            
            item.transform.GetComponent<Image>().sprite = ScinManager.GetCell();
            item.GetComponent<Image>().color = new Color(1, 1, 1, 0.55f);
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
               // color = touchZone.GetComponent<TouchZone>().currentColor;
                break;
            }
        }

        if (targetIndex == -1 || numBoxWithColl == -1)
        {
            return false; 
        }

        int zeroPoint = targetIndex - numBoxWithColl - (BlockInShape.matrixLength * (int)(numBoxWithColl / BlockInShape.matrixLength));

        int x = zeroPoint + 10 * (int)(listOfIndexs[0] / BlockInShape.matrixLength) + listOfIndexs[0] % BlockInShape.matrixLength;

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
                //Debug.Log("linr = " + line + " != " + (x / 10 - listOfIndexs[i] / 5));
                return false;
            }

        }
        for (int i = 0; i < listOfIndexs.Count; i++)
        {
            x = zeroPoint + 10 * (int)(listOfIndexs[i] / BlockInShape.matrixLength) + listOfIndexs[i] % BlockInShape.matrixLength;
            field.GetChild(x).GetComponent<Cell>().SetValue(true);
            field.GetChild(x).GetComponent<Image>().sprite = touchZone.GetChild(0).GetComponent<Image>().sprite;
            
            field.GetChild(x).GetComponent<Image>().color = Color.white;
        }
        
        scoreManager.IncreaseScore(listOfIndexs.Count);
        return true;
    }

    //востановить базовый цвет и спрайт не занятых ячеек поля
    public void CleerFieldColor()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).GetComponent<Cell>().isSet)
            {
                transform.GetChild(i).GetComponent<Image>().sprite = ScinManager.GetCell();
                transform.GetChild(i).GetComponent<Image>().color = new Color(1,1,1,0.55f);
            }
        }
    }


    //проверяем возможность размещения текущей фигуры на игровом поле
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

        if (counter == 0)
            return false;

        return true;
    }

    //проверка на проиграш
    public void CheckForLoss()
    {
        //количество вариантов размещения фигур которые есть в волне
        int count = 0;

        //если количество
        if (touchZonesParent.childCount > 1)
        {
            for (int i = 0; i < touchZonesParent.childCount; i++)
            {
                //проверка на возможность размещения фигуры
                if (CheckThePossibilityOfPlacement(touchZonesParent.GetChild(i)))
                {
                    count++;
                }
            }
            //если вариантов размещения нет то активируем 
            if (count == 0)
            {
                RevivePanel.GetComponent<TimerForRevive>().ResetTimer();
                RevivePanel.SetActive(true);
            }
        }
    }


    //сброс игрового поля до базового состояния
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


    //получить текущее состояние игрового поля в виде двумерного массива
    public static int[,] GetCurrentFieldState()
    {
        int[,] state = new int[10, 10];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j  < 10; j++)
            {
                if (field.transform.GetChild(i * 10 + j).GetComponent<Cell>().isSet)
                    state[i, j] = 1;
                else
                    state[i, j] = 0;
            }
        }
        return state;
    }
}

