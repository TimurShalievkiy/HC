using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PllayingAlong : MonoBehaviour
{

    private void Start()
    {
        int[,] field = { { 0,0,0,1,0,0,0,0,0,0 },
                         { 1,1,1,1,1,1,1,1,1,1 },
                         { 0,0,0,1,0,0,0,0,0,0 },
                         { 0,0,0,1,0,0,0,0,0,0 },
                         { 0,0,0,1,0,0,0,0,0,0 },
                         { 0,0,0,1,0,0,0,0,0,0 },
                         { 1,1,1,1,1,1,1,1,1,1 },
                         { 0,0,0,1,0,0,0,0,0,0 },
                         { 0,0,0,1,0,0,0,0,0,0 },
                         { 1,1,1,1,1,1,1,1,1,1 }};


        int[,] shape = { {0,0,0,0,0},
                         {0,1,1,1,0},
                         {0,1,1,1,0},
                         {0,1,1,1,0},
                         {0,0,0,0,0}   };


        //ChekShapeForPlacement(field, shape);

        CheckFieldForFullLines(field);
    }


    public bool ChekShapeForPlacement(int[,] field,int[,]shape)
    {
        List<int> listOfIndexs = new List<int>();

        int counter = 0;
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
                    //Debug.Log(i * shape.GetLength(1) + j);
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

                    if (field[x/ field.GetLength(0), x - (x / field.GetLength(0))* field.GetLength(0)] != 0)
                    {
                        flag = false;
                        break;
                    }
                    if (line != (x / field.GetLength(0) - item / shape.GetLength(0)))
                    {
                        //Debug.Log("line ! = " + x);
                        flag = false;
                        break;
                    }
                    
                }

                //if(flag)
                //    Debug.Log(x);
            }         
        }
      

        return true;
    }

    public void CheckFieldForFullLines(int[,] field)
    {
        bool v = true;
        bool h = true;

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
                Debug.Log("v = " + i);

            if (h)
                Debug.Log("h = " + i);
        }
    }
}
