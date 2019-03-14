using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PllayingAlong : MonoBehaviour
{
    List<int[,]> ListAllShapes;


    private void Start()
    {
        ListAllShapes = ShapesManager.GetAllShapes();

    }
    public void GetShapesAfterRevive()
    {
        int counter = 0;

        int[,] field = FieldManager.GetCurrentFieldState();

        List<ResultShape> listResultShapes = GetListResultShapesByField(field, ref counter);



        foreach (ResultShape item in listResultShapes)
        {
            item.listResultShapes = GetListResultShapesByField(FieldCondition.CheckAndRemoveFullLines(FieldCondition.PlaceShape(field, item.shapePos)), ref counter);

            //foreach (var item2 in item.listResultShapes)
            //{
            //    item2.listResultShapes = GetListResultShapesByField(FieldCondition.CheckAndRemoveFullLines(FieldCondition.PlaceShape(field, item2.shapePos)), ref counter);
            //}

            //int countOfAllLine = item.countOfFullLine;
            //int max = countOfAllLine;

            //ResultShape buff = new ResultShape(-1, 0, null);
            //foreach (ResultShape item2 in item.listResultShapes)
            //{

            //    countOfAllLine += item.countOfFullLine;
            //    countOfAllLine += item2.countOfFullLine;
            //    if (countOfAllLine > max)
            //    {
            //        max = countOfAllLine;
            //        buff = item2;
            //    }
            //    countOfAllLine = 0;
            //}

            //item.ShowResultShape();

            //buff.ShowResultShape();

            //FieldCondition.ShowField(field);

            //int[,] q = FieldCondition.PlaceShape(field, item.shapePos);
            //int w = 0;
            //FieldCondition.ShowField(q);

            //q = FieldCondition.RemoveFullLines(q, FieldCondition.CheckFieldForFullLines(q, out w));
            //FieldCondition.ShowField(q);
            //if (buff.shapePos != null)
            //{
            //Debug.Log("buff.shapePos = " + buff.shapePos);
            //Debug.Log("buff.countOfFullLine = " + buff.countOfFullLine);
            //    q = FieldCondition.PlaceShape(q, buff.shapePos);
            //FieldCondition.ShowField(q);
            //FieldCondition.ShowField(FieldCondition.RemoveFullLines(q, FieldCondition.CheckFieldForFullLines(q, out w)));

           
            //}
            //Debug.Log("--------------------------------------------- ");
            //Debug.Log("max = " + max);
            //Debug.Log("--------------------------------------------- ");
        }

        Debug.Log("counter = " + counter);
    }

    public List<ResultShape> GetListResultShapesByField(int[,] field, ref int counter)
    {

        List<List<int>> li = new List<List<int>>();
        List<ResultShape> listResultShapes = new List<ResultShape>();

        int[,] newField;

        for (int i = 0; i < ListAllShapes.Count; i++)
        {

            li = FieldCondition.ChekShapeForPlacement(field, ListAllShapes[i]);
            int countOfFullLIne = 0;




            foreach (var item in li)
            {
                counter++;
                newField = FieldCondition.PlaceShape((int[,])field.Clone(), item);
                newField = FieldCondition.RemoveFullLines(newField, FieldCondition.CheckFieldForFullLines(newField, out countOfFullLIne));

                if (countOfFullLIne > 0)
                {
                    listResultShapes.Add(new ResultShape(i, countOfFullLIne, item));
                }

            }

        }

        return listResultShapes;
    }

}

public class ResultShape
{
    public int id;
    public List<int> shapePos;
    public int countOfFullLine;
    public List<ResultShape> listResultShapes;

    public ResultShape(int id, int countOfFullLine, List<int> shapePos)
    {
        this.id = id;
        this.countOfFullLine = countOfFullLine;
        this.shapePos = shapePos;
    }

    public void ShowResultShape()
    {
        if(shapePos!=null)
        Debug.Log("id = " + id + " firstSq = " + shapePos[0] + " countOfFullLine = " + countOfFullLine);
    }
}



