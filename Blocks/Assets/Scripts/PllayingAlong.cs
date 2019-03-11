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
                         {0,0,0,0,0}   };


        //ChekShapeForPlacement(field, shape);

        //CheckFieldForFullLines(field);
        FieldCondition fc = new FieldCondition();
        fc.field = field;
        //fc.CheckFieldForFullLines(fc.field);
        // fc.RemoveFullLines(field);
        fc.ShowField();
        Debug.Log("first state = " + fc.CheckFieldForFullLines(fc.field));
        fc.PlaceShape(fc.ChekShapeForPlacement(field, shape));
        fc.ShowField();
        fc.CheckFieldForFullLines(fc.field);
        fc.RemoveFullLines(fc.field);
        fc.ShowField();
        Debug.Log("second state = " + fc.CheckFieldForFullLines(fc.field));

    }


    


}
