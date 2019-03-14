using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        }

        foreach (ResultShape item in listResultShapes)
        {
            item.GetCountOfAllFullLine();
            item.GetShapeWithMaxBBlock();

        }
        //разобраться в получении фигур количества 
        if (listResultShapes != null && listResultShapes.Count > 0)
        {
            int max = listResultShapes.Max(x => x.countOfAllFullLine);


            List<ResultShape> listResult = listResultShapes.FindAll(x => x.countOfAllFullLine == max);

            max = 0;

            foreach (var item in listResult)
            {
                if (item.ShapeWithMaxBlock != null)
                    if (max < item.shapePos.Count + item.ShapeWithMaxBlock.shapePos.Count)
                    {
                        max = item.shapePos.Count + item.ShapeWithMaxBlock.shapePos.Count;
                    }
            }

            ResultShape shapes = null;

            foreach (var item in listResult)
            {
                if (item.ShapeWithMaxBlock != null)
                    if ((item.shapePos.Count + item.ShapeWithMaxBlock.shapePos.Count) == max)
                    {
                        shapes = item;
                    }
            }


            shapes.ShowResultShape();

            if (shapes.ShapeWithMaxBlock != null)
                shapes.ShapeWithMaxBlock.ShowResultShape();
            else
                Debug.Log("null");
        }
        //добавить последней фигуру которая помещается в поле или единичный блок

        //Debug.Log("counter = " + counter);
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
    public int countOfAllFullLine;
    public List<ResultShape> listResultShapes;
    public ResultShape ShapeWithMaxBlock;

    public ResultShape(int id, int countOfFullLine, List<int> shapePos)
    {
        this.id = id;
        this.countOfFullLine = countOfFullLine;
        this.shapePos = shapePos;
    }

    public void ShowResultShape()
    {
        if (shapePos != null)
            Debug.Log("id = " + id + " firstSq = " + shapePos.Count + " countOfFullLine = " + countOfFullLine);
    }

    public void GetCountOfAllFullLine()
    {
        // проверить метод на правильность
        int countOfAllLine;
        int max = 0;
        countOfAllLine = countOfFullLine;
        max = countOfAllLine;

        ResultShape buff = new ResultShape(-1, 0, null);
        foreach (ResultShape item2 in listResultShapes)
        {

            countOfAllLine += item2.countOfFullLine;
            if (countOfAllLine > max)
            {
                max = countOfAllLine;
                buff = item2;
            }
            countOfAllLine = countOfFullLine;
        }
        countOfAllFullLine = max;
    }

    public void GetShapeWithMaxBBlock()
    {
        if (listResultShapes == null && listResultShapes.Count == 0)
            ShapeWithMaxBlock = null;


        int max = 0;
        foreach (var item in listResultShapes)
        {
            if (item.shapePos.Count > max)
                max = item.shapePos.Count;
        }

        ShapeWithMaxBlock = listResultShapes.Find(x => x.shapePos.Count == max);
    }

}



//foreach (var item2 in item.listResultShapes)
//{
//    item2.listResultShapes = GetListResultShapesByField(FieldCondition.CheckAndRemoveFullLines(FieldCondition.PlaceShape(field, item2.shapePos)), ref counter);
//}



// item.ShowResultShape();

// buff.ShowResultShape();

//FieldCondition.ShowField(field);

//int[,] q = FieldCondition.PlaceShape(field, item.shapePos);
//int w = 0;
//FieldCondition.ShowField(q);

//q = FieldCondition.RemoveFullLines(q, FieldCondition.CheckFieldForFullLines(q, out w));
//FieldCondition.ShowField(q);
//if (buff.shapePos != null)
//{
//    q = FieldCondition.PlaceShape(q, buff.shapePos);
//    FieldCondition.ShowField(q);
//    FieldCondition.ShowField(FieldCondition.RemoveFullLines(q, FieldCondition.CheckFieldForFullLines(q, out w)));


//}
//Debug.Log("--------------------------------------------- ");
//Debug.Log("max = " + max);
//Debug.Log("--------------------------------------------- ");