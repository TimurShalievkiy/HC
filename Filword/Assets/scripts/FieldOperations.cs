using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class FieldOperations 
{
    public static void AddWordToField(int numOfLetters)
    {
        if (FieldGenerator.freeCeelsZones[0].Count == 0)
        {
            Debug.Log("End");
            return;
        }
            

        int countOfAddedWord = GetMaxValue()+1;
        CheckTupicalCell();

        int startCell = 0;
        //-------------------получение стартовой ячейки-----------------------------
        if (FieldGenerator.freeCeelsZones.Count > 0)
        {
            
            if (FieldGenerator.deadEndCell.Count > 0)
            {
                int indexOfZone = Random.Range(0, FieldGenerator.deadEndCell.Count);
                startCell = FieldGenerator.deadEndCell[indexOfZone];
            }
            else
            {

                List<int> indexOfCells = new List<int>();
                for (int i = 0; i < FieldGenerator.freeCeelsZones.Count; i++)
                {
                   
                    if (FieldGenerator.freeCeelsZones[i].Count >= numOfLetters)
                        indexOfCells.Add(i);
                }

                if (indexOfCells.Count <= 0)
                {
                    Debug.Log("Break indexOfCells.Count <= 0 " + indexOfCells.Count);
                    return;
                }

                int index = indexOfCells[Random.Range(0, indexOfCells.Count)];
                int count = FieldGenerator.freeCeelsZones[index].Count;
                startCell = FieldGenerator.freeCeelsZones[index][Random.Range(0, count)];

                if (count < numOfLetters)
                {
                    Debug.Log("BREAK free cells count les then numbers of letters");
                    return;
                }

            }
        }
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^


        int x = startCell;
        int[,] buffMas = new int[FieldGenerator.field.GetLength(0), FieldGenerator.field.GetLength(1)];


        //int numOfLetters = GetNumberLetersInWord(GetCountOfCellInZoneByNumberOfCell(startCell));
        // Debug.Log("numOfLetters = " + numOfLetters);
       // SetValueByNumber(countOfAddedWord, x);

        ReturnToPreMass(FieldGenerator.field, ref buffMas);
        SetValueByNumber(countOfAddedWord, x);
        for (int i = 0; i < numOfLetters - 1; i++)
        {
            x = GetNextCell(x);
            if (x == -1)
            {
                ReturnToPreMass(FieldGenerator.field, ref buffMas);
                AddWordToField(numOfLetters);
                break;
            }
            SetValueByNumber(countOfAddedWord, x);
        }

        //if (CheckMinCountCellInZone(min))
        //{
        //    ResetFillWord();
        //    return;
        //}



    }

    public static  void GetAllFreeCellsZones()
    {
   
        if(FieldGenerator.freeCeelsZones != null)
            FieldGenerator.freeCeelsZones.Clear();
        else
            FieldGenerator.freeCeelsZones = new List<List<int>>();

        for (int i = 0; i < FieldGenerator.field.GetLength(0); i++)
        {
            for (int j = 0; j < FieldGenerator.field.GetLength(1); j++)
            {
                if (FieldGenerator.field[i, j] == 0)
                {

                    int cellNum = GetNumberByPosInArray(i, j);
                    if (!FindCellInList(cellNum))
                    {
                        
                        FieldGenerator.freeCeelsZones.Add(new List<int>());
                        FieldGenerator.freeCeelsZones[FieldGenerator.freeCeelsZones.Count-1].Add(cellNum);
                        CheckFreeNearestCells(cellNum);

                    }
                }
            }
        }
    }

    static void  CheckFreeNearestCells(int number)
    {
        int i = number / FieldGenerator.field.GetLength(1);
        int j = number - i * FieldGenerator.field.GetLength(1);
        //проверка вурхней ячейки на пустоту и запись
        if (i - 1 >= 0)
        {
            //int up = (mass.GetLength(0) * (i - 1)) + j;
            int up = GetNumberByPosInArray(i - 1, j);
            if (GetValueByNubber(up) == 0 && !FindCellInList(up))
            {

                FieldGenerator.freeCeelsZones[FieldGenerator.freeCeelsZones.Count-1].Add(up);
                CheckFreeNearestCells(up);
                //Debug.Log("Added in CheckNearest " + up);
            }
        }
        //проверка нижней ячейки на пустоту и запись
        if (i + 1 < FieldGenerator.field.GetLength(0))
        {
            //int down = (mass.GetLength(0) * (i + 1)) + j;
            int down = GetNumberByPosInArray(i + 1, j);

            if (GetValueByNubber(down) == 0 && !FindCellInList(down))
            {

                FieldGenerator.freeCeelsZones[FieldGenerator.freeCeelsZones.Count-1].Add(down);
                CheckFreeNearestCells(down);
                //Debug.Log("Added in CheckNearest " + down);
            }
        }
        //проверка левой ячейки на пустоту и запись
        if (j - 1 >= 0)
        {
            // int left = (mass.GetLength(0) * i) + j - 1;
            int left = GetNumberByPosInArray(i, j - 1);

            if (GetValueByNubber(left) == 0 && !FindCellInList(left))
            {
                FieldGenerator.freeCeelsZones[FieldGenerator.freeCeelsZones.Count-1].Add(left);
                CheckFreeNearestCells(left);
                //Debug.Log("Added in CheckNearest " + left);
            }
        }
        //проверка правой ячейки на пустоту и запись
        if (j + 1 < FieldGenerator.field.GetLength(1))
        {
            //int right = (mass.GetLength(0) * i) + j + 1;
            int right = GetNumberByPosInArray(i, j + 1);

            if (GetValueByNubber(right) == 0 && !FindCellInList(right))
            {
                FieldGenerator.freeCeelsZones[FieldGenerator.freeCeelsZones.Count-1].Add(right);
                CheckFreeNearestCells(right);
                //Debug.Log("Added in CheckNearest " + right);
            }
        }
    }

    static int GetNumberByPosInArray(int i, int j)
    {
        //Debug.Log("i = " + i + " j = " + j);
        return i * FieldGenerator.field.GetLength(1) + j;
    }

    static int GetValueByNubber(int number)
    {
        int i = number / FieldGenerator.field.GetLength(1);
        int j = number - i * FieldGenerator.field.GetLength(1);

        // Debug.Log("number = " + number + " i = " + i + " j = " + j);
        return FieldGenerator.field[i, j];
    }

    static bool FindCellInList(int number)
    {
        if (FieldGenerator.freeCeelsZones == null)
            return false;
        if (FieldGenerator.freeCeelsZones.Count == 0)
            return false;

        for (int i = 0; i < FieldGenerator.freeCeelsZones.Count; i++)
        {
            for (int j = 0; j < FieldGenerator.freeCeelsZones[i].Count; j++)
                if (FieldGenerator.freeCeelsZones[i][j] == number) { return true; }
        }

        return false;
    }

    static int GetNextCell(int numberCurrentCell)
    {
        int i = numberCurrentCell / FieldGenerator.field.GetLength(1);
        int j = numberCurrentCell - i * FieldGenerator.field.GetLength(1);

        int[] dir = { -1, -1, -1, -1 };

        int index = 0;


        if (i - 1 >= 0)
        {
            int up = GetNumberByPosInArray(i - 1, j);


            //Debug.Log("Current = " + numberCurrentCell + " Up cell = " + up);
            if (GetValueByNubber(up) == 0)
            {
                if (CountFreeNearestCell(up) == 1)
                    return up;
                // Debug.Log("Up cell Free ");
                dir[index] = up;
                index++;
            }
        }

        if (i + 1 < FieldGenerator.field.GetLength(0))
        {
            int down = GetNumberByPosInArray(i + 1, j);



            if (GetValueByNubber(down) == 0)
            {
                //Debug.Log("down cell Free ");
                if (CountFreeNearestCell(down) == 1)
                    return down;
                dir[index] = down;
                index++;
            }
        }

        if (j - 1 >= 0)
        {
            //int left = (mass.GetLength(0) * i) + j - 1;
            int left = GetNumberByPosInArray(i, j - 1);



            if (GetValueByNubber(left) == 0)
            {
                //Debug.Log("left cell Free ");
                if (CountFreeNearestCell(left) == 1)
                    return left;
                dir[index] = left;
                index++;
            }
        }

        if (j + 1 < FieldGenerator.field.GetLength(1))
        {
            //int right = (mass.GetLength(0) * i) + j + 1;
            int right = GetNumberByPosInArray(i, j + 1);



            if (GetValueByNubber(right) == 0)
            {
                //Debug.Log("right cell Free ");
                if (CountFreeNearestCell(right) == 1)
                    return right;
                dir[index] = right;
                index++;
            }
        }
        if (index == 0)
            return -1;
        
        int index2 = Random.Range(0, index);
        //Debug.Log(index);
        //Debug.Log("----"+index2);
        //Debug.Log("index2 = " + index2 + " index1 = " + index);
        return dir[index2];
    }

    static int CountFreeNearestCell(int number)
    {
        int i = number / FieldGenerator.field.GetLength(1);
        int j = number - i * FieldGenerator.field.GetLength(1);

        int count = 0;

        //проверка вурхней ячейки на пустоту и запись
        if (i - 1 >= 0)
        {
            //int up = (mass.GetLength(0) * (i - 1)) + j;
            int up = GetNumberByPosInArray(i - 1, j);
            if (GetValueByNubber(up) == 0)
            {
                count++;
            }
        }
        //проверка нижней ячейки на пустоту и запись
        if (i + 1 < FieldGenerator.field.GetLength(0))
        {
            //int down = (mass.GetLength(0) * (i + 1)) + j;
            int down = GetNumberByPosInArray(i + 1, j);
            if (GetValueByNubber(down) == 0)
            {
                count++;
            }
        }
        //проверка левой ячейки на пустоту и запись
        if (j - 1 >= 0)
        {
            // int left = (mass.GetLength(0) * i) + j - 1;
            int left = GetNumberByPosInArray(i, j - 1);
            if (GetValueByNubber(left) == 0)
            {
                count++;
            }
        }
        //проверка правой ячейки на пустоту и запись
        if (j + 1 < FieldGenerator.field.GetLength(1))
        {
            //int right = (mass.GetLength(0) * i) + j + 1;
            int right = GetNumberByPosInArray(i, j + 1);
            if (GetValueByNubber(right) == 0)
            {
                count++;
            }
        }
        return count;
    }

    static void CheckTupicalCell()
    {
        if (FieldGenerator.deadEndCell != null)
            FieldGenerator.deadEndCell.Clear();
        else
            FieldGenerator.deadEndCell = new List<int>();

        for (int i = 0; i < FieldGenerator.freeCeelsZones.Count; i++)
        {
            for (int j = 0; j < FieldGenerator.freeCeelsZones[i].Count; j++)
            {
                if (CountFreeNearestCell(FieldGenerator.freeCeelsZones[i][j]) == 1)
                {
                    FieldGenerator.deadEndCell.Add(FieldGenerator.freeCeelsZones[i][j]);
                }
            }
        }
    }

    static void SetValueByNumber(int value, int number)
    {
        int i = number / FieldGenerator.field.GetLength(1);
        int j = number - i * FieldGenerator.field.GetLength(1);
        FieldGenerator.field[i, j] = value;

    }

    static void ReturnToPreMass(int[,] x, ref int[,] y)
    {
        for (int i = 0; i < x.GetLength(0); i++)
        {
            for (int j = 0; j < x.GetLength(1); j++)
            {
                y[i, j] = x[i, j];
            }
        }
    }

    static int GetMaxValue()
    {
        int max = 0;
        for (int i = 0; i < FieldGenerator.field.GetLength(0); i++)
        {
            for (int j = 0; j < FieldGenerator.field.GetLength(1); j++)
            {
                if (max < FieldGenerator.field[i, j])
                    max = FieldGenerator.field[i, j];
            }
          
        }
        return max;
    }

    public static void ShowFreeCellsZones()
    {
        string s = "count = " + FieldGenerator.freeCeelsZones.Count + "\n";
        for (int i = 0; i < FieldGenerator.freeCeelsZones.Count; i++)
        {
            foreach (var item in FieldGenerator.freeCeelsZones[i])
            {
                s += item + " ";
            }
            s += "\n";
        }
        Debug.Log(s);
    }

    public static void ShowMassInDebugLog()
    {
        string s2 = "";
        for (int i = 0; i < FieldGenerator.field.GetLength(0); i++)
        {
            for (int j = 0; j < FieldGenerator.field.GetLength(1); j++)
            {
                s2 += FieldGenerator.field[i, j].ToString() + " ";
            }
            s2 += "\n";
        }
        Debug.Log(s2);
    }

    public static void SetColor()
    {
        GameObject g = GameObject.Find("images");
        Color c = new Color();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                //if(FieldGenerator.field[i, j]!=0)
                //Debug.Log(FieldGenerator.field[i, j]);
                if(FieldGenerator.field[i, j]!=0)
                g.transform.GetChild(i * 10 + j).GetChild(0).transform.GetComponent<Text>().text = FieldGenerator.field[i, j].ToString();
                //g.transform.GetChild(i * 10 + j).GetComponent<Image>().color = c;
            }
            
        }
        
    }
}
