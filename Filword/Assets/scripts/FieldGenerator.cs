using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldGenerator : MonoBehaviour
{
    bool started = true;

    

   public static int[,] field;

    public static List<List<int>> freeCeelsZones;

    public static List<int> deadEndCell;

    public void GenerateField()
    {
        // freeCeelsZones = new List<List<int>>();

        //   field = new int[,] { 
        //{ 0,0,0,1,0,0,1,0,0,0},
        //{ 1,1,1,1,0,0,1,0,1,0},
        //{ 0,0,0,1,0,1,1,1,0,0},
        //{ 0,0,0,1,0,0,0,0,0,0},
        //{ 1,1,0,1,1,1,1,1,1,1},
        //{ 0,1,0,1,0,0,0,0,0,0},
        //{ 0,1,0,1,0,0,0,0,0,0},
        //{ 0,1,0,1,0,0,0,1,0,0},
        //{ 0,1,0,1,0,0,0,1,0,0},
        //{ 0,0,0,1,0,0,0,1,0,0}
        //};
        field = new int[10,10];

    }
    public void AddWord()
    {
        if (started)
        {
            GenerateField();
            started = false;
        }


        FieldOperations.GetAllFreeCellsZones();


        //получеем количество и длины всех доступных слов со словаря
        //выбираем рандомный элемент с учетом проверки на допустимость расположения последующих

        int x = Random.Range(3, 11);
        Debug.Log("length of word = " + x);
        FieldOperations.AddWordToField(x);


        FieldOperations.GetAllFreeCellsZones();
        FieldOperations.ShowFreeCellsZones();
        FieldOperations.ShowMassInDebugLog();
        FieldOperations.SetColor();
    }


}
