using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCondition : MonoBehaviour
{
    public int[,] field;
    public int numberOfFullLines;
    public List<int> listOfIndexesInFuulLine;

    public int CheckFieldForFullLines(int[,] field)
    {
        int numOfFullLine = 0;
        bool v = true;
        bool h = true;
        listOfIndexesInFuulLine = new List<int>();

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

                numOfFullLine++;
            }


            if (h)
            {
                 foreach (int item in GetListOfIndexesByHorizontal(i))
                {
                    if (listOfIndexesInFuulLine.IndexOf(item) == -1)
                        listOfIndexesInFuulLine.Add(item);
                }
                numOfFullLine++;
            }
                
        }

        return numOfFullLine;
    }

    public void RemoveFullLines(int[,] field)
    {
        foreach (var item in listOfIndexesInFuulLine)
        {
            field[item / field.GetLongLength(0), item - (item / field.GetLongLength(0)) * field.GetLongLength(0)] = 0;
        }
    }

    List<int> GetListOfIndexesByVertical(int numVertical)
    {
        List<int> listFullCells = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            listFullCells.Add(i * field.GetLength(1) + numVertical);
        }
        return listFullCells;
    }

    List<int> GetListOfIndexesByHorizontal(int numHorizontal)
    {
        List<int> listFullCells = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            listFullCells.Add(numHorizontal * 10 + i);
        }
        return listFullCells;
    }


    public void PlaceShape(List<int> shape)
    {
        if (shape == null)
            return;
      
        foreach (var item in shape)
        {
            field[item / field.GetLongLength(0), item - (item / field.GetLongLength(0)) * field.GetLongLength(0)] = 1;
        }
    }

    public List<int> ChekShapeForPlacement(int[,] field, int[,] shape)
    {
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

                List<int> x2 = new List<int>();
                foreach (var item in listOfIndexs)
                {


                    x = zeroPoint + 10 * (int)(item / length) + item % length;

                    x2.Add(x);


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

                    return x2;
                }
            }
        }


        return null;
    }

    public void ShowField()
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
}
