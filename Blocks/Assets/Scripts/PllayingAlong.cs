using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PllayingAlong : MonoBehaviour
{

    private void Start()
    {
        int[,] field = { { 0,0,0,1,0,0,0,0,0,0 },
                         { 1,1,1,1,1,1,1,1,1,1 },
                         { 1,1,1,1,0,0,0,1,1,1 },
                         { 0,1,0,1,0,0,0,1,0,0 },
                         { 0,0,0,1,0,0,0,0,1,0 },
                         { 0,0,0,1,0,1,0,1,0,0 },
                         { 1,1,1,1,1,1,1,1,1,1 },
                         { 0,0,0,1,0,0,0,0,0,0 },
                         { 0,0,0,1,0,0,0,0,0,0 },
                         { 1,1,1,1,1,1,1,1,1,1 }};


        int[,] shape = { {0,0,0,0,0},
                         {0,1,1,1,0},
                         {0,1,1,1,0},
                         {0,1,1,1,0},
                         {0,0,0,0,0}};


        //ChekShapeForPlacement(field, shape);

        //CheckFieldForFullLines(field);

        //fc.CheckFieldForFullLines(fc.field);
        // fc.RemoveFullLines(field);
        //fc.ShowField();

        //fc.PlaceShape(fc.ChekShapeForPlacement(field, shape));
        //fc.ShowField();
        //fc.CheckFieldForFullLines(fc.field);
        //fc.RemoveFullLines(fc.field);
        //fc.ShowField();

    }
    public void GetShapesAfterRevive()
    {
        int counter = 0;
        FieldCondition fc = new FieldCondition();
        int[,] field = FieldManager.GetCurrentFieldState();
        //int[,] field = new int[,] { { 0,0,0,1,0,0,0,0,0,0 },
        //                            { 1,0,1,0,1,1,1,1,1,1 },
        //                            { 1,1,1,1,0,0,0,1,1,1 },
        //                            { 0,0,0,1,0,0,0,1,0,0 },
        //                            { 0,0,0,1,0,0,0,0,1,0 },
        //                            { 0,0,0,1,0,1,0,1,0,0 },
        //                            { 1,1,1,0,1,1,1,1,1,1 },
        //                            { 0,0,0,1,0,0,0,0,0,0 },
        //                            { 0,0,0,1,0,0,0,0,0,0 },
        //                            { 1,1,1,0,1,1,1,1,1,1 }
        //};

        //FieldCondition.ShowField(field);

        List<int[,]> ls = ShapesManager.GetAllShapes();


        List<List<int>> li = new List<List<int>>();
        List<List<int>> li2 = new List<List<int>>();
        int[,] newField;
        int[,] newField2;
        List<int> xList = new List<int>();
        for (int i = 0; i < ls.Count; i++)
        {

            li = FieldCondition.ChekShapeForPlacement(field, ls[i]);
            int x = 0;



            
            foreach (var item in li)
            {
                counter++;
                newField = FieldCondition.PlaceShape((int[,])field.Clone(), item);
                newField = FieldCondition.RemoveFullLines(newField, FieldCondition.CheckFieldForFullLines(newField, out x));

                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //работать только с теми которые дают зачеркивание линий
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++

                //Debug.Log(x);
                if (x>0)
                    xList.Add(x);
                //Debug.Log("field");
                //FieldCondition.ShowField(field);
                //Debug.Log("newField");
                //FieldCondition.ShowField(newField);
                //if(item.Count>0)
                // Debug.Log(item.Count);
                //for (int j = 0; j < ls.Count; j++)
                //{

                //    li2 = FieldCondition.ChekShapeForPlacement(newField, ls[j]);

                //    //Debug.Log("i = " + i + " j = " + j + " count = " + li2.Count);
                //    foreach (var item2 in li2)
                //    {
                //        
                //        newField2 = FieldCondition.PlaceShape((int[,])newField.Clone(), item);
                //         newField2 = FieldCondition.RemoveFullLines(newField, FieldCondition.CheckFieldForFullLines(newField, out x));

                //    }

                //}
            }
            
        }
        string s = "";
        foreach (var item in xList)
        {
            s += item + " ";
        }
            

        Debug.Log(s);
        
        Debug.Log("counter = " + counter);
    }




    //string s = "num = " + (i+1) + " count = " + li.Count + "\n";
    //Debug.Log(s);
}



