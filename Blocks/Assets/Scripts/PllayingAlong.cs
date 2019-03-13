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



        for (int i = 0; i < ls.Count; i++)
        {

            li = FieldCondition.ChekShapeForPlacement(field, ls[i]);
            int x = 0;
            int[,] newField;

            foreach (var item in li)
            {

                newField = FieldCondition.PlaceShape((int[,])field.Clone(), item);
                newField = FieldCondition.RemoveFullLines(newField, FieldCondition.CheckFieldForFullLines(newField, out x));
                //Debug.Log("field");
                //FieldCondition.ShowField(field);
                //Debug.Log("newField");
                //FieldCondition.ShowField(newField);
                //for (int j = 0; j < ls.Count; j++)
                //{
                    
                   
                //}
            }

        }
    }




    //string s = "num = " + (i+1) + " count = " + li.Count + "\n";
    //Debug.Log(s);
}



