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
        fc.field = FieldManager.GetCurrentFieldState();
        fc.ShowField();

        List<int[,]> ls = ShapesManager.GetAllShapes();

        for (int i = 0; i < ls.Count; i++)
        {
            fc.ChekShapeForPlacement(fc.field, ls[i]);
        }

    }




}
