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
        //список с ячейками находящимися в полных линиях
        List<GameObject> listFullCells = new List<GameObject>();

        //флаг что горизонтальная линия заполнена
        bool horizontal = true;
        //флаг что вертикальная линия заполнена 
        bool vertical = true;

        //проход по игровому полю с проверкой на заполненость линий
        for (int i = 0; i < 10; i++)
        {
            //выставляем значения флагов в начальные 
            horizontal = true;
            vertical = true;


            //проход по всем ячейкам
            for (int j = 0; j < 10; j++)
            {
                //если ячейка с заданным индексом не пуста то текущей горизонтальной линии присваем флаг фолс
                if (!field.GetChild(i * 10 + j).transform.GetComponent<Cell>().isSet)
                    horizontal = false;

                //если ячейка с заданным индексом не пуста то текущей вертикальной линии присваем флаг фолс
                if (!field.GetChild(j * 10 + i).transform.GetComponent<Cell>().isSet)
                    vertical = false;
            }

            //если флаг заполненой горизонтальной линии равен тру
            if (horizontal)
                //добавляем ячейку в список ячеек с заполненой линии с проверкой на наличие в списке
                foreach (GameObject item in ReturnListOfCellsByHorizontal(i))
                {
                    if (listFullCells.IndexOf(item) == -1)
                        listFullCells.Add(item);
                }

            //если флаг заполненой вертикальной линии равен тру
            if (vertical)
                //добавляем ячейку в список ячеек с заполненой линии с проверкой на наличие в списке
                foreach (GameObject item in ReturnListOfCellsByVertical(i))
                {
                    if (listFullCells.IndexOf(item) == -1)
                        listFullCells.Add(item);
                }
        }

        //возвращаем ячейки из списка в базовое состояние
        foreach (var item in listFullCells)
        {
            //item.transform.GetComponent<Image>().color = ColorManager.GetDefaultColour();
            

            //получаем спрайт
            item.transform.GetComponent<Image>().sprite = ScinManager.GetCell();


            //выствляем прозрачность
            item.GetComponent<Image>().color = new Color(1, 1, 1, 0.55f);

            //параметр занята ли ячейка выствляем в фолс 
            item.transform.GetComponent<Cell>().SetValue(false);
        }

        //увеличиваем счет соответсвенно количеству ячеек в списке ячеек с полных линий
        scoreManager.IncreaseScore(listFullCells.Count);
    }

    //получить список всех заполненых ячеек по номру горизонтальной линии 
    List<GameObject> ReturnListOfCellsByHorizontal(int numHorizontal)
    {
        List<GameObject> listFullCells = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            listFullCells.Add(field.GetChild(numHorizontal * 10 + i).gameObject);
        }
        return listFullCells;
    }


    //получить список всех заполненых ячеек по номру вертикальной линии 
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
        //возвращаем цвет ячеек игрового поля на базовый
        CleerFieldColor();

        //размерность массива фигуры
        int length = BlockInShape.matrixLength;

        //индекс блока с коллайдером 
        int numBoxWithColl = -1;

        //индекс цели на которую попал коллайдер
        int targetIndex = -1;


        //список с индексами задействованных блоков в фигуре
        List<int> listOfIndexs = new List<int>();

        //заполнение списка зайдествованных блоков в фигуре
        for (int i = 0; i < touchZone.childCount; i++)
        {
            //если активен компонент Image
            if (touchZone.GetChild(i).GetComponent<Image>().enabled)
            {
                //добавляем элемент в список
                listOfIndexs.Add(touchZone.GetChild(i).GetSiblingIndex());
            }
        }

        //получаем индекс бокса с колайдером
        for (int i = 0; i < touchZone.childCount; i++)
        {
            //если компонент BoxCollider2D активен
            if (touchZone.GetChild(i).GetComponent<BoxCollider2D>().isActiveAndEnabled)
            {
                //присваеваем индекс
                numBoxWithColl = i;

                //получаем индекс цели
                targetIndex = touchZone.GetChild(i).GetComponent<BoxCollider2D>().GetComponent<BlockInShape>().TargetIndex;
                break;
            }
        }

        //если один из параметров не присвоен прерываем метд
        if (targetIndex == -1 || numBoxWithColl == -1)
        {
            return false; 
        }

        //получаем кординаты нулевой точки массива относительно поля
        //для этого от индекса цели отнимаем индекс блока с коллайдером и отнимаем  (размер матрицы умноженный на номер блока разделенный на размер матрицы)
        //нулевая точка нам нужна для определения индукса блоков в фигуре относительно поля и пересекшему его блока с коллайдером
        int zeroPoint = targetIndex - numBoxWithColl - (BlockInShape.matrixLength * (int)(numBoxWithColl / BlockInShape.matrixLength));

        //находим индекс первого элемента в списке относительно игрового поля
        int x = zeroPoint + 10 * (int)(listOfIndexs[0] / BlockInShape.matrixLength) + listOfIndexs[0] % BlockInShape.matrixLength;


        //номер линии нужен для анализа не выходит ли фигура за рамки игрового поля
        int line = (x / 10 - listOfIndexs[0] / 5);

        //проверка на соответвие всем требованиям по размещению фигуры
        for (int i = 0; i < listOfIndexs.Count; i++)
        {
            //находим индекс блока фигуры в поле
            x = zeroPoint + 10 * (int)(listOfIndexs[i] / BlockInShape.matrixLength) + listOfIndexs[i] % BlockInShape.matrixLength;
            //если больше 99 или меньше 0 возвращаем фолс
            if (x > 99 || x < 0)
                return false;

            //если ячейка заполнена возвращаем фолс
            if (field.GetChild(x).GetComponent<Cell>().isSet)
            {
                return false;
            }

            //проверка на то не выходит ли фигура за рамки игрового поля
            if (line != (x / 10 - listOfIndexs[i] / 5))
            {
                return false;
            }

        }

        //если все проверки пройдены 
        for (int i = 0; i < listOfIndexs.Count; i++)
        {
            //получаем индекс в игровом поле
            x = zeroPoint + 10 * (int)(listOfIndexs[i] / BlockInShape.matrixLength) + listOfIndexs[i] % BlockInShape.matrixLength;
            //задаем значение что ячейка занята
            field.GetChild(x).GetComponent<Cell>().SetValue(true);
            //ставим новый спрайт
            field.GetChild(x).GetComponent<Image>().sprite = touchZone.GetChild(0).GetComponent<Image>().sprite;
            //ставим цвет в базовый убирая прозачность
            field.GetChild(x).GetComponent<Image>().color = Color.white;
        }
        //увеличиваем счетна количество равное количеству блоков в фигуре
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

