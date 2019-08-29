using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix 
{
    public int[,] matrixArray;

    List<List<int>> wordsCellsPosition;
    List<List<int>> buffList;

    public int xRange;
    public int yRange;

    int minLength;
    int maxLength;

    public Matrix(int[,] matrixArray, List<List<int>> wordsCellsPosition)
    {
        this.matrixArray = matrixArray;
        xRange = matrixArray.GetLength(0);
        yRange = matrixArray.GetLength(1);

        this.wordsCellsPosition = wordsCellsPosition;
    }

    #region operations

    public void RandRotationAndMirror()
    {
        //rotation
        if (xRange < yRange)
        {
            int x = Random.Range(1, 100);
            if (x % 2 == 0)
            {
                Debug.Log(" x < y RoteteMatrixLeft180()");
                matrixArray = RoteteMatrixLeft180();
            }
        }
        else
        {
            int x = Random.Range(1, 100);
            if (x > 0 && x <= 25)
            {
                matrixArray = RoteteMatrixLeft90();
                Debug.Log("RoteteMatrixLeft90()");
            }
               
            if (x > 25 && x <= 50)
            {
                Debug.Log("RoteteMatrixLeft180()");
                matrixArray = RoteteMatrixLeft180();
            }
               
            if (x > 50 && x < 75)
            {
                matrixArray = RoteteMatrixLeft180();
                matrixArray = RoteteMatrixLeft90();
                Debug.Log("RoteteMatrixLeft270()");
            }
               
        }


        if (xRange < yRange)
        {
            int x = Random.Range(1, 100);
            if (x % 2 == 0)
            {
                matrixArray = MirrorMatrixHorisontal();
                Debug.Log(" x < y MirrorMatrixHorisontal()");
            }
        }
        else
        {
            int x = Random.Range(1, 100);
            if (x > 0 && x <= 50)
            {
                Debug.Log("MirrorMatrixHorisontal()");
                matrixArray = MirrorMatrixHorisontal();
            }

            if (x > 50 && x <= 100)
            {
                Debug.Log("MirrorMatrixVertical()");
                matrixArray = MirrorMatrixVertical();
            }
                


        }
        
    }

   int[,] RoteteMatrixLeft90()
    {
        int length1 = matrixArray.GetLength(0);
        int length2 = matrixArray.GetLength(1);
        int[,] buff = new int[length1, length2];

        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {               
                buff[i, j] = matrixArray[j, length2 - 1-i];
                ReplaceLeterPosInList(i,j,j, length2 - 1 - i);
            }
        }


        buffList = null;
        return buff;
    }

    int[,] RoteteMatrixLeft180()
    {
        int length1 = matrixArray.GetLength(0);
        int length2 = matrixArray.GetLength(1);

        int[,] buff = new int[length1, length2];

        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                buff[i, j] = matrixArray[length1 - i - 1, length2 - j - 1];
                ReplaceLeterPosInList(i, j, length1 - i - 1, length2 - j - 1);
            }
        }



        return buff;
    }

    int[,] MirrorMatrixHorisontal()
    {
        int length1 = matrixArray.GetLength(0);
        int length2 = matrixArray.GetLength(1);


        int[,] buff = new int[length1, length2];


        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                buff[i, j] = matrixArray[length1 - i - 1, j];
                ReplaceLeterPosInList(i, j, length1 - i - 1, j);
            }
        }

        return buff;
    }

    int[,] MirrorMatrixVertical()
    {
        int length1 = matrixArray.GetLength(0);
        int length2 = matrixArray.GetLength(1);


        int[,] buff = new int[length1, length2];


        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                buff[i, j] = matrixArray[i, length2 - j - 1];
                ReplaceLeterPosInList(i,j,i, length2 - j - 1);
            }
        }

        return buff;
    }

    void ReplaceLeterPosInList(int x, int y, int i2, int j2)
    {
        int startPos  = GetNumberByPosInArray(i2,j2);
        int endPos = GetNumberByPosInArray(x, y);
        if (buffList == null)
        {
            buffList = new List<List<int>>();
            for (int i = 0; i < wordsCellsPosition.Count; i++)
            {
                buffList.Add(new List<int>());
                for (int j = 0; j < wordsCellsPosition[i].Count; j++)
                    buffList[i].Add(-1);
            }
        }
        
        for (int i = 0; i < wordsCellsPosition.Count; i++)
        {
            for (int j = 0; j < wordsCellsPosition[i].Count; j++)
            {
                if (wordsCellsPosition[i][j] == endPos && buffList[i][j]==-1)
                {
                    wordsCellsPosition[i][j] = startPos;
                    buffList[i][j] = startPos;
                    break;
                }               
            }  
        }




    }

     int GetNumberByPosInArray(int i, int j)
    {
        //Debug.Log("i = " + i + " j = " + j);
        return i * matrixArray.GetLength(1) + j;
    }

    public void ShowMatrix(int[,] arr)
    {
        int length1 = arr.GetLength(0);
        int length2 = arr.GetLength(1);
        string s = "";
        for (int i = 0; i < length1; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                s += arr[i, j] + " ";
            }
            s += "\n";
        }
        Debug.Log(s);
    }

    public void ShowWordPos()
    {
        string s = "";
        for (int i = 0; i < wordsCellsPosition.Count; i++)
        {
            for (int j = 0; j < wordsCellsPosition[i].Count; j++)
            {
                s += wordsCellsPosition[i][j] + " ";
            }
            s += "\n";
        }
        Debug.Log(s);
    }

    #endregion operations
}
