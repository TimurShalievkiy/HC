using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PllayingAlong : MonoBehaviour
{
    List<Shape> listAllShapes;
    public TouchZonesCreator zonesCreator;
    int[] shapesId = new int[] { 0, 0, 0 };

    private void Start()
    {
        listAllShapes = ShapesManager.GetAllShapes();
        //shapesId = new int[] { 0, 0, 0 };
    }
    public void GetShapesAfterRevive()
    {
        //shapesId = new int[] { 0, 0, 0 };
        ShapesManager.GetRandomShapeWave();
        zonesCreator.GenerateNewWaveOfShapeAfterRevive(shapesId);

    }


    public void GetShapesWithHelp()
    {

        FieldCondition.GetAllFreeZones();
        //GetShapesAfterRevive2();
        //int x = 0;
        //int summ = 0;
        //foreach (var item in ShapesManager.GetAllShapes())
        //{
        //    List<List<int>> listREsult = FieldCondition.ChekShapeForPlacement(FieldManager.GetCurrentFieldState(), item.array);
        //    Debug.Log(listREsult.Count);
        //    summ += listREsult.Count;
        //}
        //Debug.Log("summ = " + summ);

    }


    public List<ResultShape> GetListResultShapesByField(int[,] field, ref int counter)
    {

        List<List<int>> li = new List<List<int>>();
        List<ResultShape> listResultShapes = new List<ResultShape>();

        int[,] newField;

        for (int i = 0; i < listAllShapes.Count; i++)
        {

            li = FieldCondition.ChekShapeForPlacement(field, listAllShapes[i].array);
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


    public void GetShapesAfterRevive2()
    {
        shapesId = new int[] { 0, 0, 0 };
        int counter = 0;


        int[,] field = FieldManager.GetCurrentFieldState();

        List<ResultShape> listResultShapes = GetListResultShapesByField(field, ref counter);


        if (listResultShapes != null && listResultShapes.Count > 0)
        {
            foreach (ResultShape item in listResultShapes)
            {
                item.listResultShapes = GetListResultShapesByField(FieldCondition.CheckAndRemoveFullLines(FieldCondition.PlaceShape(field, item.shapePos)), ref counter);
            }

            foreach (ResultShape item in listResultShapes)
            {
                item.InitAllValue();
            }


            int max = listResultShapes.Max(x => x.countOfAllFullLine);

            List<ResultShape> listResult = listResultShapes.FindAll(x => x.countOfAllFullLine == max);




            

            if (listResult != null && listResult.Count > 0)
            {

                max = listResult.Max(x => x.countOfBlockWithMaxSHape);
                listResult = listResult.FindAll(x => x.countOfBlockWithMaxSHape == max);



                ResultShape res;
                if (listResult.Count > 1)
                {
                    res = listResult[Random.Range(0, listResult.Count)];
                }
                else
                    res = listResult[0];

                shapesId[0] = res.id;
                res.ShowResultShape();
                if (res.ShapeWithMaxBlock != null)
                {
                    shapesId[1] = res.ShapeWithMaxBlock.id;
                    res.ShapeWithMaxBlock.ShowResultShape();
                }



            }
        }


        //zonesCreator.GenerateNewWaveOfShapeAfterRevive(shapesId);
      //  Debug.Log(counter);
      //  Debug.Log(shapesId[0] + " " + shapesId[1] + " " + shapesId[2]);

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





