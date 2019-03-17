using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class PllayingAlong : MonoBehaviour
{
    List<int[,]> ListAllShapes;
    public TouchZonesCreator zonesCreator;
    int[] shapesId = new int[] { 0, 0, 0 };

    private void Start()
    {
        ListAllShapes = ShapesManager.GetAllShapes();
        //shapesId = new int[] { 0, 0, 0 };
    }
    public void GetShapesAfterRevive()
    {
        //shapesId = new int[] { 0, 0, 0 };
        zonesCreator.GenerateNewWaveOfShapeAfterRevive(shapesId);

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
    public int countOfBlockWithMaxSHape;
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
            Debug.Log("id = " + id + " firstPos = " + shapePos[0]+ " shapePos.Count = " + shapePos.Count + " countOfFullLine = " + countOfFullLine + " countOfAllFullLine = " + countOfAllFullLine + " countOfBlockWithMaxSHape = " + countOfBlockWithMaxSHape);
    }

    public void GetCountOfAllFullLine()
    {
        // проверить метод на правильность
        int countOfAllLine;
        int max = 0;
        countOfAllLine = countOfFullLine;
        max = countOfAllLine;


        foreach (ResultShape item2 in listResultShapes)
        {

            countOfAllLine += item2.countOfFullLine;
            if (countOfAllLine > max)
            {
                max = countOfAllLine;
            }
            countOfAllLine = countOfFullLine;
        }
 
        countOfAllFullLine = max;

       
    }
    public void OptimizeSecondShapeList()
    {
        if (listResultShapes != null && listResultShapes.Count > 0)
        {
            int max = listResultShapes.Max(x => x.countOfFullLine);
            listResultShapes = listResultShapes.FindAll(x => x.countOfFullLine == max);

            if (listResultShapes.Count > 0)
            {
                max = listResultShapes.Max(x => x.shapePos.Count);
                listResultShapes = listResultShapes.FindAll(x => x.shapePos.Count == max);
            }
        }
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


    public void InitValueCountOfBlockWithMaxSHape()
    {
        countOfBlockWithMaxSHape = shapePos.Count;
        if (ShapeWithMaxBlock != null)
            countOfBlockWithMaxSHape += ShapeWithMaxBlock.shapePos.Count;

    }
    public void InitAllValue()
    {
        GetCountOfAllFullLine();
        OptimizeSecondShapeList();
        GetShapeWithMaxBBlock();
        InitValueCountOfBlockWithMaxSHape();
    }
}





//public void GetShapesAfterRevive()
//{
//    shapesId = new int[] { 0, 0, 0 };





//    //=================================================




//    //Debug.Log("countMin = "+ countMin + " countMax = " + countMax);
//    //Debug.Log(x / 1000);

//    //string path = "Assets/Resources/test.txt";

//    ////Write some text to the test.txt file
//    //StreamWriter writer = new StreamWriter(path, true);
//    //writer.WriteLine(s);
//    //writer.Close();





//    //=================================================



//    // int[,] field = FieldManager.GetCurrentFieldState();

//    // List<ResultShape> listResultShapes = GetListResultShapesByField(field, ref counter);


//    //if (listResultShapes != null && listResultShapes.Count > 0)
//    //{
//    //    foreach (ResultShape item in listResultShapes)
//    //    {
//    //        item.listResultShapes = GetListResultShapesByField(FieldCondition.CheckAndRemoveFullLines(FieldCondition.PlaceShape(field, item.shapePos)), ref counter);
//    //    }

//    //    foreach (ResultShape item in listResultShapes)
//    //    {
//    //        item.InitAllValue();
//    //    }


//    //    int max = listResultShapes.Max(x => x.countOfAllFullLine);

//    //    List<ResultShape> listResult = listResultShapes.FindAll(x => x.countOfAllFullLine == max);




//    //    //разобраться в получении фигур количества 

//    //    if (listResult != null && listResult.Count > 0)
//    //    {

//    //        max = listResult.Max(x => x.countOfBlockWithMaxSHape);
//    //        listResult = listResult.FindAll(x => x.countOfBlockWithMaxSHape == max);



//    //        ResultShape res;
//    //        if (listResult.Count > 1)
//    //        {
//    //            res = listResult[Random.Range(0, listResult.Count)];
//    //        }
//    //        else
//    //            res = listResult[0];

//    //        shapesId[0] = res.id;
//    //        res.ShowResultShape();
//    //        if (res.ShapeWithMaxBlock != null)
//    //        {
//    //            shapesId[1] = res.ShapeWithMaxBlock.id;
//    //            res.ShapeWithMaxBlock.ShowResultShape();
//    //        }



//    //    }
//    //}


//    zonesCreator.GenerateNewWaveOfShapeAfterRevive(shapesId);


//}