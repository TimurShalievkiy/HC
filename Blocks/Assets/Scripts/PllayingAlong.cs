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
        ShapesManager.GetRandomShapeWave();
        zonesCreator.GenerateNewWaveOfShapeAfterRevive(shapesId);
    }


    public void GetShapesWithHelp()
    {

        List<List<int>> freeZones = FieldCondition.GetAllFreeZones();
        List<ResultShape> equalsZero = new List<ResultShape>();
        List<ResultShape> moreThanZero = new List<ResultShape>();



        bool allIn = true;
        int id = 0;
 
        foreach (var item in ShapesManager.GetAllShapes())
        {
            List<List<int>> listREsult = FieldCondition.ChekShapeForPlacement(FieldManager.GetCurrentFieldState(), item.array);
    
            for (int n = 0; n < freeZones.Count; n++)
            {
                for (int i = 0; i < listREsult.Count; i++)
                {
                    allIn = true;
                    for (int j = 0; j < listREsult[i].Count; j++)
                    {
                        
                        if (!freeZones[n].Exists(x => x == listREsult[i][j]))
                        {
                            allIn = false;
                            break;
                        }                       
                    }
                    if (allIn)
                    {
                        if (listREsult[i].Count - freeZones[n].Count == 0)
                            equalsZero.Add(new ResultShape(id, 0, listREsult[i]));
                        else if (freeZones[n].Count - listREsult[i].Count > 0)
                            moreThanZero.Add(new ResultShape(id,0, listREsult[i]));
                    }
                }
            }

            id++;
        }

        equalsZero = equalsZero.Distinct(new ItemEqualityComparer()).ToList();


        if (equalsZero.Count >= 3)
        {
            ResultShape x1 = equalsZero[Random.Range(0, equalsZero.Count)];
            equalsZero.Remove(x1);

            ResultShape x2 = equalsZero[Random.Range(0, equalsZero.Count)];
            equalsZero.Remove(x2);

            ResultShape x3 = equalsZero[Random.Range(0, equalsZero.Count)];
            zonesCreator.GenerateNewWaveOfShapeAfterRevive(new int[] { x1.id, x2.id, x3.id });
        }
        else
        {
            moreThanZero.Reverse();

            List<ResultShape> listResult = new List<ResultShape>();
            List<ResultShape> listForRemuve = new List<ResultShape>();

            for (int i = 0; i < equalsZero.Count; i++)
            {
                listResult.Add(equalsZero[i]);
            }

            if (listResult.Count == 2)
            {
                for (int i = 0; i < moreThanZero.Count; i++)
                {
                    if (IsCrossing(listResult, moreThanZero[i]))
                    {
                        listForRemuve.Add(moreThanZero[i]);
                    }

                }
                for (int i = 0; i < listForRemuve.Count; i++)
                {
                    moreThanZero.Remove(listForRemuve[i]);
                }
                List<ResultShape> shapes = moreThanZero.FindAll(x => x.id != listResult[0].id && x.id != listResult[1].id);

                ResultShape shape;
                if (shapes == null || shapes.Count == 0)
                {
                    shape = moreThanZero[Random.Range(0, moreThanZero.Count)];
                }
                else
                {
                    shape = shapes[Random.Range(0, shapes.Count)];
                }
                listResult.Add(shape);


                zonesCreator.GenerateNewWaveOfShapeAfterRevive(new int[] { listResult[0].id, listResult[1].id, listResult[2].id });
                return;
            }
            else if (listResult.Count == 1) 
            {
                for (int i = 0; i < moreThanZero.Count; i++)
                {
                    if (IsCrossing(listResult, moreThanZero[i]))
                    {
                        listForRemuve.Add(moreThanZero[i]);
                    }

                }

                for (int i = 0; i < listForRemuve.Count; i++)
                {
                    moreThanZero.Remove(listForRemuve[i]);
                }

                List<ResultShape> shapes = moreThanZero.FindAll(x => x.id != listResult[0].id);
                shapes.Reverse();

                ResultShape shape;
                ResultShape shape2;

                bool flag = true;

                if (shapes.Distinct(new ItemEqualityComparer()).ToList().Count > 2)
                {
                    while (flag)
                    {
                        shape = shapes[Random.Range(0, shapes.Count)];
                        shape2 = shapes[Random.Range(0, shapes.Count)];

                        if (shape.id == shape2.id)
                            continue;

                        if (!IsCrossing(shape2, shape))
                        {
                            flag = false;
                            listResult.Add(shape);
                            listResult.Add(shape2);
                            Debug.Log(shape2.id + " " + shape2.shapePos[0] + " " + shape.id + " " + shape.shapePos[0]);
                            break;
                        }
                    }
                }
                else
                {
                    shape = shapes[Random.Range(0, shapes.Count)];
                    shape2 = shapes[Random.Range(0, shapes.Count)];
                    listResult.Add(shape);
                    listResult.Add(shape2);             
                }

                zonesCreator.GenerateNewWaveOfShapeAfterRevive(new int[] { listResult[0].id, listResult[1].id, listResult[2].id });

            }
            else
            {            
                List<ResultShape> shapes = moreThanZero;
                shapes.Reverse();
 
                ResultShape shape;
                ResultShape shape2;
                ResultShape shape3;
                bool flag = true;

                if (shapes.Distinct(new ItemEqualityComparer()).ToList().Count > 3)
                {
                    while (flag)
                    {
                        shape = shapes[Random.Range(0, shapes.Count)];
                        shape2 = shapes[Random.Range(0, shapes.Count)];
                        shape3 = shapes[Random.Range(0, shapes.Count)];

                        if (shape.id == shape2.id || shape3.id == shape2.id || shape3.id == shape.id)
                            continue;

                        if (!IsCrossing(shape2, shape) && !IsCrossing(shape3, shape) && !IsCrossing(shape2, shape3))
                        {
                            flag = false;
                            listResult.Add(shape);
                            listResult.Add(shape2);
                            listResult.Add(shape3);
                            break;
                        }
                    }
                }
                else
                {
                    List<ResultShape> res = shapes.Distinct(new ItemEqualityComparer()).ToList();
                    ResultShape x1 = res[Random.Range(0, res.Count)];
                    res.Remove(x1);

                    ResultShape x2 =res[Random.Range(0, res.Count)];
                    res.Remove(x2);

                    ResultShape x3 =res[Random.Range(0, res.Count)];
                    res.Remove(x3);
                    zonesCreator.GenerateNewWaveOfShapeAfterRevive(new int[] { x1.id, x2.id, x3.id });
                    return;

                }

                zonesCreator.GenerateNewWaveOfShapeAfterRevive(new int[] { listResult[0].id, listResult[1].id, listResult[2].id });
                return;
            }

        }
    }

    bool IsCrossing(List<ResultShape> listResult, ResultShape resultShape)
    {
        foreach (ResultShape item in listResult)
        {
            for (int i = 0; i < item.shapePos.Count; i++)
            {
                for (int j = 0; j < resultShape.shapePos.Count; j++)
                {
                    if (item.shapePos.Exists(x => x == resultShape.shapePos[j]))
                        return true;
                }
            }
        }

        return false;
    }

    bool IsCrossing(ResultShape firstShape, ResultShape seconShaoe)
    {

            for (int i = 0; i < firstShape.shapePos.Count; i++)
            {
                for (int j = 0; j < seconShaoe.shapePos.Count; j++)
                {
                    if (firstShape.shapePos.Exists(x => x == seconShaoe.shapePos[j]))
                        return true;
                }
            }
        

        return false;
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


    //public void GetShapesAfterRevive2()
    //{
    //    shapesId = new int[] { 0, 0, 0 };
    //    int counter = 0;


    //    int[,] field = FieldManager.GetCurrentFieldState();

    //    List<ResultShape> listResultShapes = GetListResultShapesByField(field, ref counter);


    //    if (listResultShapes != null && listResultShapes.Count > 0)
    //    {
    //        foreach (ResultShape item in listResultShapes)
    //        {
    //            item.listResultShapes = GetListResultShapesByField(FieldCondition.CheckAndRemoveFullLines(FieldCondition.PlaceShape(field, item.shapePos)), ref counter);
    //        }

    //        foreach (ResultShape item in listResultShapes)
    //        {
    //            item.InitAllValue();
    //        }


    //        int max = listResultShapes.Max(x => x.countOfAllFullLine);

    //        List<ResultShape> listResult = listResultShapes.FindAll(x => x.countOfAllFullLine == max);




            

    //        if (listResult != null && listResult.Count > 0)
    //        {

    //            max = listResult.Max(x => x.countOfBlockWithMaxSHape);
    //            listResult = listResult.FindAll(x => x.countOfBlockWithMaxSHape == max);



    //            ResultShape res;
    //            if (listResult.Count > 1)
    //            {
    //                res = listResult[Random.Range(0, listResult.Count)];
    //            }
    //            else
    //                res = listResult[0];

    //            shapesId[0] = res.id;
    //            res.ShowResultShape();
    //            if (res.ShapeWithMaxBlock != null)
    //            {
    //                shapesId[1] = res.ShapeWithMaxBlock.id;
    //                res.ShapeWithMaxBlock.ShowResultShape();
    //            }



    //        }
    //    }


    //    //zonesCreator.GenerateNewWaveOfShapeAfterRevive(shapesId);
    //  //  Debug.Log(counter);
    //  //  Debug.Log(shapesId[0] + " " + shapesId[1] + " " + shapesId[2]);

    //}
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



class ItemEqualityComparer : IEqualityComparer<ResultShape>
{
    public bool Equals(ResultShape x, ResultShape y)
    {
        // Two items are equal if their keys are equal.
        return x.id == y.id;
    }

    public int GetHashCode(ResultShape obj)
    {
        return obj.id.GetHashCode();
    }
}

