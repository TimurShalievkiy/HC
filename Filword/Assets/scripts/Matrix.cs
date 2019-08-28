using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix 
{
    public int[,] matrixArray;

    List<List<int>> wordsCellsPosition;

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

    public void Func()
    {
        // RoteteMatrixLeft();

        ShowMatrix(RoteteMatrixLeft90());
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
            }
        }



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
            }
        }



        return new int[1, 1];
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
            }
        }

        return buff;
    }

    public void ShowMatrix(int[,] arr)
    {
        int length1 = arr.GetLength(0);
        int length2 = arr.GetLength(1);
        string s  = "";
        for (int i = 0; i < length1 ; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                s += arr[i, j] + " ";
            }
            s += "\n";
        }
        Debug.Log(s);
    }

    #endregion operations
}
