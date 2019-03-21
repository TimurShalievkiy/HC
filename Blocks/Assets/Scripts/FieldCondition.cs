using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCondition : MonoBehaviour
{
   static List<List<int>> listFreeZonesOfCell = new List<List<int>>();
    static int rankOfListFreeZones = 0;
    static int[,] currentFieldState;
   // public int numberOfFullLines;

    
    public static List<int> CheckFieldForFullLines(int[,] field, out int countOfFullLine)
    {
        countOfFullLine = 0;
        bool v = true;
        bool h = true;
        List<int>  listOfIndexesInFuulLine = new List<int>();

        for (int i = 0; i < field.GetLength(0); i++)
        {
            v = true;
            h = true;
            for (int j = 0; j < field.GetLength(1); j++)
            {
                if (v)
                {
                    if (field[j, i] == 0)
                        v = false;
                }

                if (h)
                {
                    if (field[i, j] == 0)
                        h = false;
                }
            }
            if (v)
            {
                foreach (int item in GetListOfIndexesByVertical(i))
                {
                    if (listOfIndexesInFuulLine.IndexOf(item) == -1)
                        listOfIndexesInFuulLine.Add(item);
                }

                countOfFullLine++;
            }


            if (h)
            {
                 foreach (int item in GetListOfIndexesByHorizontal(i))
                {
                    if (listOfIndexesInFuulLine.IndexOf(item) == -1)
                        listOfIndexesInFuulLine.Add(item);
                }
                countOfFullLine++;
            }
                
        }

        return listOfIndexesInFuulLine;
    }

    public static int[,] RemoveFullLines(int[,] field, List<int> listOfIndexesInFuulLine)
    {
        foreach (var item in listOfIndexesInFuulLine)
        {
            field[item / field.GetLongLength(0), item - (item / field.GetLongLength(0)) * field.GetLongLength(0)] = 0;
        }
        return field;
    }

    static List<int> GetListOfIndexesByVertical(int numVertical)
    {
        List<int> listFullCells = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            listFullCells.Add(i * 10 + numVertical);
        }
        return listFullCells;
    }

    static List<int> GetListOfIndexesByHorizontal(int numHorizontal)
    {
        List<int> listFullCells = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            listFullCells.Add(numHorizontal * 10 + i);
        }
        return listFullCells;
    }

    public static int[,] PlaceShape(int[,] field2,List<int> shape)
    {
        int[,] field = (int[,])field2.Clone();
        if (shape == null)
            return field;
      
        foreach (var item in shape)
        {
            field[item / field.GetLongLength(0), item - (item / field.GetLongLength(0)) * field.GetLongLength(0)] = 1;
        }
        return field;
    }

    public static List<List<int>> ChekShapeForPlacement(int[,] field, int[,] shape)
    {
        List<List<int>> listREsult = new List<List<int>>();

        List<int> listOfIndexs = new List<int>();
        


        int numBoxWithColl = -1;
        int length = shape.GetLength(0);
        int zeroPoint = -1;

        bool flag = true;


        for (int i = 0; i < shape.GetLength(0); i++)
        {
            for (int j = 0; j < shape.GetLength(1); j++)
            {
                if (shape[i, j] == 1)
                {
                    listOfIndexs.Add(i * shape.GetLength(0) + j);
                }
            }
        }


        numBoxWithColl = listOfIndexs[0];

        if (numBoxWithColl == -1)
        {
            return null;
        }


        //==============================================
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            { 
              

                if (field[i, j] != 0)
                    continue;


                flag = true;

                zeroPoint = i * field.GetLength(0) + j - numBoxWithColl - (length * (int)(numBoxWithColl / length));
                //Debug.Log(zeroPoint);
                int x = zeroPoint + 10 * (int)(listOfIndexs[0] / length) + listOfIndexs[0] % length;

                int line = (x / field.GetLength(0) - listOfIndexs[0] / shape.GetLength(0));

                listREsult.Add(new List<int>());

                foreach (var item in listOfIndexs)
                {
                    x = zeroPoint + 10 * (int)(item / length) + item % length;

                    listREsult[listREsult.Count - 1].Add(x);

                    if (x > 99 || x < 0)
                    {

                        flag = false;
                        break;
                    }

                    if (field[x / field.GetLength(0), x - (x / field.GetLength(0)) * field.GetLength(0)] != 0)
                    {
                        flag = false;
                        break;
                    }
                    if (line != (x / field.GetLength(0) - item / shape.GetLength(0)))
                    {
                        flag = false;
                        break;
                    }

                }
                if (flag)
                {

                }
                else {
                    
                    listREsult.Remove(listREsult[listREsult.Count - 1]);
                }
            }
        }


        return listREsult;
    }

    public static bool ChekShapeForPlacement(int[,] shape)
    {
        List<int> listOfIndexs = new List<int>();
        int[,] field = FieldManager.GetCurrentFieldState();


        int numBoxWithColl = -1;
        int length = shape.GetLength(0);
        int zeroPoint = -1;

        bool flag = true;


        for (int i = 0; i < shape.GetLength(0); i++)
        {
            for (int j = 0; j < shape.GetLength(1); j++)
            {
                if (shape[i, j] == 1)
                {
                    listOfIndexs.Add(i * shape.GetLength(0) + j);
                }
            }
        }


        numBoxWithColl = listOfIndexs[0];

        if (numBoxWithColl == -1)
        {
            return false;
        }


        //==============================================
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {


                if (field[i, j] != 0)
                    continue;


                flag = true;

                zeroPoint = i * field.GetLength(0) + j - numBoxWithColl - (length * (int)(numBoxWithColl / length));

                int x = zeroPoint + 10 * (int)(listOfIndexs[0] / length) + listOfIndexs[0] % length;

                int line = (x / field.GetLength(0) - listOfIndexs[0] / shape.GetLength(0));

                foreach (var item in listOfIndexs)
                {
                    x = zeroPoint + 10 * (int)(item / length) + item % length;

                    if (x > 99 || x < 0)
                    {

                        flag = false;
                        break;
                    }

                    if (field[x / field.GetLength(0), x - (x / field.GetLength(0)) * field.GetLength(0)] != 0)
                    {
                        flag = false;
                        break;
                    }
                    if (line != (x / field.GetLength(0) - item / shape.GetLength(0)))
                    {
                        flag = false;
                        break;
                    }

                }
                if (flag)
                {
                    return flag;
                }
            }
        }


        return false;
    }

    public static void ShowField(int [,] field)
    {
        string str = "";
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                str += field[i, j] + " ";
            }
            str += "\n";
        }
        Debug.Log(str);
    }

    public static int[,] CheckAndRemoveFullLines(int[,] field)
    {
        int x = 0;
        List<int> listOfIndexesInFuulLine = CheckFieldForFullLines(field, out x);
        foreach (var item in listOfIndexesInFuulLine)
        {
            field[item / field.GetLongLength(0), item - (item / field.GetLongLength(0)) * field.GetLongLength(0)] = 0;
        }
        return field;
    }

    public static List<List<int>> GetAllFreeZones()
    {
        currentFieldState = FieldManager.GetCurrentFieldState();
        listFreeZonesOfCell = new List<List<int>>();
        rankOfListFreeZones = 0;

        for (int i = 0; i < currentFieldState.GetLength(0); i++)
        {
            for (int j = 0; j < currentFieldState.GetLength(1); j++)
            {
                if (currentFieldState[i, j] == 0)
                {
                    if (!IsInListFreeZonesOfCell(i, j))
                    {
                        listFreeZonesOfCell.Add(new List<int>());
                        listFreeZonesOfCell[rankOfListFreeZones].Add(i * currentFieldState.GetLength(0) + j);
                        CheckNearestCell(i, j);
                        rankOfListFreeZones++;
                    }
                }
                
            }
        }

        //string s = "";
        //Debug.Log(listFreeZonesOfCell.Count);
        //for (int i = 0; i < listFreeZonesOfCell.Count; i++)
        //{
        //    for (int j = 0; j < listFreeZonesOfCell[i].Count; j++)
        //    {
        //        s += listFreeZonesOfCell[i][j] + " ";
        //    }
        //    s += "\n";
        //}
        //Debug.Log(s);
        return listFreeZonesOfCell;
    }


    public static void CheckNearestCell(int i, int j)
    {
        //проверка вурхней ячейки на пустоту и запись
        if (i - 1 >= 0)
        {
            int up = (currentFieldState.GetLength(0) * (i - 1)) + j;
            
            if (currentFieldState[i-1,j] == 0 && !IsInListFreeZonesOfCell( i-1, j))
            {

                listFreeZonesOfCell[rankOfListFreeZones].Add(up);
                CheckNearestCell(i-1,j);
                //Debug.Log("Added in CheckNearest " + up);
            }
        }

        //проверка нижней ячейки на пустоту и запись
        if (i + 1 < currentFieldState.GetLength(0))
        {
            int down = (currentFieldState.GetLength(0) * (i + 1)) + j;
            //int down = GetNumberByPosInArray(i + 1, j);
           // Debug.Log((FieldManager.GetCurrentFieldState()[i + 1, j] == 0) +  " " + !IsInListFreeZonesOfCell(i + 1, j));
            if (currentFieldState[i+1, j] == 0 && !IsInListFreeZonesOfCell(i + 1, j))
            {
               // Debug.Log("add = " + i + " " + j);
                listFreeZonesOfCell[rankOfListFreeZones].Add(down);
                CheckNearestCell(i+1 , j);
                //Debug.Log("Added in CheckNearest " + down);
            }
        }

        //проверка левой ячейки на пустоту и запись
        if (j - 1 >= 0)
        {
             int left = (currentFieldState.GetLength(0) * i) + j - 1;

            if (currentFieldState[i, j -1] == 0 && !IsInListFreeZonesOfCell(i, j-1))
            {
                listFreeZonesOfCell[rankOfListFreeZones].Add(left);
                CheckNearestCell(i , j-1);
            }
        }


        //проверка правой ячейки на пустоту и запись
        if (j + 1 < currentFieldState.GetLength(1))
        {
            int right = (currentFieldState.GetLength(0) * i) + j + 1;
         

            if (currentFieldState[i, j + 1] == 0 && !IsInListFreeZonesOfCell(i, j + 1))
            {
                listFreeZonesOfCell[rankOfListFreeZones].Add(right);
                CheckNearestCell(i, j + 1);
                //Debug.Log("Added in CheckNearest " + right);
            }
        }

    }

    static bool IsInListFreeZonesOfCell(int i, int j)
    {
        int num = i * currentFieldState.GetLength(0) + j;

        for (int n = 0; n < listFreeZonesOfCell.Count; n++)
        {
            for (int m = 0; m < listFreeZonesOfCell[n].Count; m++)
            {
                if (listFreeZonesOfCell[n][m] == num)
                    return true;
            }
        }


        return false;
    }

    public static int GetCountOfFreeCell(int[,] field)
    {
        int x = 0;

        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                if (field[i, j] == 0)
                    x++;
            }
        
        }

        return x;
    }
}
