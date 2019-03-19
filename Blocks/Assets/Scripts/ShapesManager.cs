using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapesManager : MonoBehaviour
{
   
    public static List<Shape> listOfShapes = new List<Shape>();

    //получить фигуру по Id с заданием родительского обьекта
    public static GameObject GetShapeById(int id,Transform parent)
    {
        //получаем заготовку фигуры
        GameObject instance = Instantiate(Resources.Load("FullShape", typeof(GameObject)), parent.position, Quaternion.identity) as GameObject;
        //назначаем родительский обьект
        instance.transform.parent = parent;
        //выставляем скейл в базовое значение
        instance.transform.localScale = new Vector3(1, 1, 1);

        //получаем фигуру из списка по айди
        int[,] shape = GetAllShapes()[id].array;

        //убираем лишние блоки в зависимости от значенией фигуры
        for (int i = 0; i < shape.GetLength(0); i++)
        {
            for (int j = 0; j < shape.GetLength(1); j++)
            {
                if (shape[i, j] == 1)
                {
                    instance.transform.GetChild(i * shape.GetLength(0) + j).GetComponent<Image>().enabled = true;
                }
                else
                    instance.transform.GetChild(i * shape.GetLength(0) + j).GetComponent<Image>().enabled = false;
            }
           
        }
        //возвращаем готовую фигуру
        return instance;

    }

    //получение списка всех фигур в виде двумерных масивов
    public static List<Shape> GetAllShapes()
    {


       



        if (listOfShapes.Count == 0)
        {
            //1
            listOfShapes.Add(new Shape(5f, new int[,] {
                             { 0,0,0,0,0},
                             { 0,0,0,0,0},
                             { 0,0,1,0,0},
                             { 0,0,0,0,0},
                             { 0,0,0,0,0} }));
            //2
            listOfShapes.Add(new Shape(5f, new int[,] {
                             { 0,0,0,0,0},
                             { 0,0,0,0,0},
                             { 0,0,1,1,0},
                             { 0,0,1,1,0},
                             { 0,0,0,0,0} }));

            //3
            listOfShapes.Add(new Shape(5f, new int[,] {
                             { 0,0,0,0,0},
                             { 0,1,1,1,0},
                             { 0,1,1,1,0},
                             { 0,1,1,1,0},
                             { 0,0,0,0,0} }));

            //4
            listOfShapes.Add(new Shape(5f,new int[,] {
                             { 0,0,0,0,0},
                             { 0,0,0,0,0},
                             { 0,1,1,0,0},
                             { 0,0,0,0,0},
                             { 0,0,0,0,0} }));

            //5
            listOfShapes.Add(new Shape(5f,new int[,] {
                             { 0,0,0,0,0},
                             { 0,0,0,0,0},
                             { 0,1,1,1,0},
                             { 0,0,0,0,0},
                             { 0,0,0,0,0} }));


            //6
            listOfShapes.Add(new Shape(5f,new int[,] {
                             { 0,0,0,0,0},
                             { 0,0,0,0,0},
                             { 0,1,1,1,1},
                             { 0,0,0,0,0},
                             { 0,0,0,0,0} }));


            //7
            listOfShapes.Add(new Shape(5f,new int[,] {
                             { 0,0,0,0,0},
                             { 0,0,0,0,0},
                             { 1,1,1,1,1},
                             { 0,0,0,0,0},
                             { 0,0,0,0,0} }));
            //8
            listOfShapes.Add(new Shape(5f,new int[,] {
                             { 0,0,0,0,0},
                             { 0,0,1,0,0},
                             { 0,0,1,0,0},
                             { 0,0,0,0,0},
                             { 0,0,0,0,0} }));

            //9
            listOfShapes.Add(new Shape(5f,new int[,] {
                             { 0,0,0,0,0},
                             { 0,0,1,0,0},
                             { 0,0,1,0,0},
                             { 0,0,1,0,0},
                             { 0,0,0,0,0} }));

            //10
            listOfShapes.Add(new Shape(5f,new int[,] {
                             { 0,0,0,0,0},
                             { 0,0,1,0,0},
                             { 0,0,1,0,0},
                             { 0,0,1,0,0},
                             { 0,0,1,0,0} }));

            //11
            listOfShapes.Add(new Shape(5f,new int[,] {
                             { 0,0,1,0,0},
                             { 0,0,1,0,0},
                             { 0,0,1,0,0},
                             { 0,0,1,0,0},
                             { 0,0,1,0,0} }));


            //12
            listOfShapes.Add(new Shape(5f,new int[,] {
                             { 0,0,0,0,0},
                             { 0,0,0,0,0},
                             { 0,0,1,1,0},
                             { 0,0,1,0,0},
                             { 0,0,0,0,0} }));


            //13
            listOfShapes.Add(new Shape(5f,new int[,] {
                             { 0,0,0,0,0},
                             { 0,0,0,0,0},
                             { 0,1,1,0,0},
                             { 0,0,1,0,0},
                             { 0,0,0,0,0} }));


            //14
            listOfShapes.Add(new Shape(5f,new int[,] {
                             { 0,0,0,0,0},
                             { 0,0,1,0,0},
                             { 0,0,1,1,0},
                             { 0,0,0,0,0},
                             { 0,0,0,0,0} }));


            //15
            listOfShapes.Add(new Shape(5f,new int[,] {
                             { 0,0,0,0,0},
                             { 0,0,1,0,0},
                             { 0,1,1,0,0},
                             { 0,0,0,0,0},
                             { 0,0,0,0,0} }));


            //16
            listOfShapes.Add(new Shape(5f,new int[,] {
                             { 0,0,0,0,0},
                             { 0,1,0,0,0},
                             { 0,1,0,0,0},
                             { 0,1,1,1,0},
                             { 0,0,0,0,0} }));

            //17
            listOfShapes.Add(new Shape(5f,new int[,] {
                             { 0,0,0,0,0},
                             { 0,0,0,1,0},
                             { 0,0,0,1,0},
                             { 0,1,1,1,0},
                             { 0,0,0,0,0} }));

            //18
            listOfShapes.Add(new Shape(5f,new int[,] {
                             { 0,0,0,0,0},
                             { 0,1,1,1,0},
                             { 0,1,0,0,0},
                             { 0,1,0,0,0},
                             { 0,0,0,0,0} }));

            //19
            listOfShapes.Add(new Shape(5f,new int[,] {
                             { 0,0,0,0,0},
                             { 0,1,1,1,0},
                             { 0,0,0,1,0},
                             { 0,0,0,1,0},
                             { 0,0,0,0,0} }));


            ////20
            //listOfSapes.Add(new int[,] {
            //                 { 0,0,0,0,0},
            //                 { 0,0,0,0,0},
            //                 { 0,0,1,1,0},
            //                 { 0,1,1,0,0},
            //                 { 0,0,0,0,0} });

            ////21
            //listOfSapes.Add(new int[,] {
            //                 { 0,0,0,0,0},
            //                 { 0,0,0,0,0},
            //                 { 0,1,1,0,0},
            //                 { 0,0,1,1,0},
            //                 { 0,0,0,0,0} });

            ////22
            //listOfSapes.Add(new int[,] {
            //                 { 0,0,0,0,0},
            //                 { 0,0,1,0,0},
            //                 { 0,0,1,1,0},
            //                 { 0,0,0,1,0},
            //                 { 0,0,0,0,0} });

            ////23
            //listOfSapes.Add(new int[,] {
            //                 { 0,0,0,0,0},
            //                 { 0,0,1,0,0},
            //                 { 0,1,1,0,0},
            //                 { 0,1,0,0,0},
            //                 { 0,0,0,0,0} });

            ////24
            //listOfSapes.Add(new int[,] {
            //                 { 0,0,0,0,0},
            //                 { 0,1,0,0,0},
            //                 { 0,1,1,1,0},
            //                 { 0,0,0,0,0},
            //                 { 0,0,0,0,0} });

            ////25
            //listOfSapes.Add(new int[,] {
            //                 { 0,0,0,0,0},
            //                 { 0,0,0,1,0},
            //                 { 0,1,1,1,0},
            //                 { 0,0,0,0,0},
            //                 { 0,0,0,0,0} });

            ////26
            //listOfSapes.Add(new int[,] {
            //                 { 0,0,0,0,0},
            //                 { 0,0,0,0,0},
            //                 { 0,1,1,1,0},
            //                 { 0,1,0,0,0},
            //                 { 0,0,0,0,0} });

            ////27
            //listOfSapes.Add(new int[,] {
            //                 { 0,0,0,0,0},
            //                 { 0,0,0,0,0},
            //                 { 0,1,1,1,0},
            //                 { 0,0,0,1,0},
            //                 { 0,0,0,0,0} });



            ////28
            //listOfSapes.Add(new int[,] {
            //                 { 0,0,0,0,0},
            //                 { 0,0,1,0,0},
            //                 { 0,1,1,1,0},
            //                 { 0,0,0,0,0},
            //                 { 0,0,0,0,0} });


            ////29
            //listOfSapes.Add(new int[,] {
            //                 { 0,0,0,0,0},
            //                 { 0,0,0,0,0},
            //                 { 0,1,1,1,0},
            //                 { 0,0,1,0,0},
            //                 { 0,0,0,0,0} });


            ////30
            //listOfSapes.Add(new int[,] {
            //                 { 0,0,0,0,0},
            //                 { 0,0,1,0,0},
            //                 { 0,0,1,1,0},
            //                 { 0,0,1,0,0},
            //                 { 0,0,0,0,0} });


            ////31
            //listOfSapes.Add(new int[,] {
            //                 { 0,0,0,0,0},
            //                 { 0,0,1,0,0},
            //                 { 0,1,1,0,0},
            //                 { 0,0,1,0,0},
            //                 { 0,0,0,0,0} });

        }





        return listOfShapes;

    }



    public static int[] GetRandomShapeWave()
    {
        float x = 0;

        int shape1 = 0;
        int shape2 = 0;
        int shape3 = 0;


        List<int> listShapesForNewWave = new List<int>();

        string s = "";
        for (int i = 0; i < 1000; i++)
        {

            shape1 = Randomizer.GetNextShapeId();
            shape2 = Randomizer.GetNextShapeId();
            shape3 = Randomizer.GetNextShapeId();


            s += shape1 + "\n";
            s += shape2 + "\n";
            s += shape3 + "\n";

            x += GetCountOfBlockByShaprID(shape1) + GetCountOfBlockByShaprID(shape2) + GetCountOfBlockByShaprID(shape3);


        }
        shape1 = Randomizer.GetNextShapeId();
        shape2 = Randomizer.GetNextShapeId();
        shape3 = Randomizer.GetNextShapeId();

        Debug.Log(s);
        Debug.Log(x/1000);
        return new int[] { shape1, shape2, shape3 };
    }
    
    //static int GetIdRandShapeByCountOfBlock(int countOfBlock, int firstShapeId = -1, int secondShapeId = -1)
    //{


    //    if (countOfBlock == 9)
    //    {
    //        return 2;
    //    }
    //    if (countOfBlock == 1 )
    //    {
    //        if(firstShapeId == 0 || secondShapeId == 0)
    //            countOfBlock = Random.Range(2, 6);
    //    }      
        
    //    if (countOfBlock == 2 && GetCountOfBlockByShaprID(firstShapeId) == 2 || GetCountOfBlockByShaprID(secondShapeId) == 2)
    //        countOfBlock = Random.Range(3,6);

    //    int id = -1;

    //    int countOfBlockInCurrentShape;

    //    int index = 0;

    //    List<int> listShapesId = new List<int>();

    //    foreach (int[,] item in GetAllShapes().)
    //    {
    //        countOfBlockInCurrentShape = 0;
    //        foreach (int item2 in item)
    //        {
    //            if(item2 == 1)
    //                countOfBlockInCurrentShape++;
    //        }
    //        if (countOfBlockInCurrentShape == countOfBlock)
    //        {
    //            listShapesId.Add(index);
    //        }
    //        index++;
    //    }

    //    if (listShapesId.Count > 0)
    //    {
    //        id = listShapesId[Random.Range(0, listShapesId.Count)];

    //    }

        
    //    return id;
    //}

    static int GetCountOfBlockByShaprID(int id)
    {
        if (id == -1)
            return 0;
        int countOfBlockInCurrentShape = 0;
        foreach (int item in GetAllShapes()[id].array)
        {

                if (item == 1)
                    countOfBlockInCurrentShape++;
         
        }

        return countOfBlockInCurrentShape;
    }
    
}


[System.Serializable]
public class Shape
{
    public int[,] array;
    public float chanse;
    public string str;

    public Shape(float chanse , int[,] array)
    {
        this.chanse = chanse;
        this.array = array;
    }
    public Shape(float chanse, string str)
    {
        this.chanse = chanse;
        this.str = str;
    }
}
