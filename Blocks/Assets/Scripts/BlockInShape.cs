using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockInShape : MonoBehaviour
{

    //размерность фигуры с блоками
    public static int matrixLength = 5;

    //ссылка на fieldManager для доступа к полю
    public FieldManager fieldManager;


    //уровень прозрачности
    float alphaLevel = 0.5f;
  
    //ссылка на цель с которой пересекся коллайдер
    Transform target;

    //индекс цели 
    int targetIndex = -1;

    public Transform Target { get => target; set => target = value; }

    public int TargetIndex { get {
            if (target != null)
                return target.GetSiblingIndex();
            else
                return -1;
        }
        set => targetIndex = value; }

    //при нахождении коллайдера в зоне с ячейкой
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Cell")
        {
            //и если ячейка не занята
            if (!collision.transform.GetComponent<Cell>().isSet)
            {
                //она становится целью
                target = collision.transform;
                //отрисовываем тень фигуры
                MakeShapeShadowInGameField();
            }
            else
            {
                //иначе убираем тень фигуры с поля
                transform.parent.GetComponent<TouchZone>().fieldManager.CleerFieldColor();
                //а цель выставляем в нул
                target = null;
            }


        }
        else
        {
            //в любых других случаях очищаем поле и выствляем цель в нул
            transform.parent.GetComponent<TouchZone>().fieldManager.CleerFieldColor();
            target = null;
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{

    //    if (collision.gameObject.name == "Cell")
    //    {
    //        if (target == null)
    //        {
    //            transform.parent.GetComponent<TouchZone>().fieldManager.CleerFieldColor();
    //        }

    //    }
    //}

    //public void SetValueForCurrentTarger()
    //{
    //    if (Target != null )
    //    {
    //        //Target.transform.GetComponent<Image>().color = ColorManager.GetNextColor();
    //        Target.transform.GetComponent<Image>().sprite = ScinManager.GetNextSq();

    //        Target.transform.GetComponent<Cell>().SetValue(true);
    //    }
    //}




    public void MakeShapeShadowInGameField()
    {
        //перед началом отрисовки тени востанавливаем состояние поля очищая все уже отрисованное
        transform.parent.GetComponent<TouchZone>().fieldManager.CleerFieldColor();

        //
        //int length = BlockInShape.matrixLength;

        //индекс блока с коллайдером 
        int numBoxWithColl = -1;

        //индекс цели
        int targetIndex = -1;

        //цвет ячейки нужен для установки прозрачности
        Color color = new Color();


        //список индексов ячеек фигуры
        List<int> listOfIndexs = new List<int>();


        //заполняем список индексов ячеек фигуры по активности компонента Image
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).GetComponent<Image>().enabled)
            {
                listOfIndexs.Add(transform.parent.GetChild(i).GetSiblingIndex());
            }
        }
        //текущий блок является блоком с коллайдером потому присваиваем ему индекс 
        numBoxWithColl = transform.GetSiblingIndex();

        //получаем индекс цели
        targetIndex = target.GetSiblingIndex();

        //цвет равен цвету текущего спрайта
        color = transform.GetComponent<Image>().color;

        //если параметры остались в базовых значениях прерываем метод
        if (targetIndex == -1 || numBoxWithColl == -1)
        {
            return ;
        }
        //получаем нулевую точку
        int zeroPoint = targetIndex - numBoxWithColl - (BlockInShape.matrixLength * (int)(numBoxWithColl / BlockInShape.matrixLength));

        //получаем индекс в поле относительно позиции в матрице
        int x = zeroPoint + 10 * (int)(listOfIndexs[0] / BlockInShape.matrixLength) + listOfIndexs[0] % BlockInShape.matrixLength;

        //номер линии нужен для проверки на выход фигуры за границы поля
        int line = (x / 10 - listOfIndexs[0] / 5);

        //проводим проверку на соответсвие всем требованиям по установке
        for (int i = 0; i < listOfIndexs.Count; i++)
        {
            x = zeroPoint + 10 * (int)(listOfIndexs[i] / BlockInShape.matrixLength) + listOfIndexs[i] % BlockInShape.matrixLength;
            if (x > 99 || x < 0)
                return ;

            if (FieldManager.field.GetChild(x).GetComponent<Cell>().isSet)
            {
                return ;
            }


            if (line != (x / 10 - listOfIndexs[i] / 5))
            {
               // Debug.Log("linr = " + line + " != " + (x / 10 - listOfIndexs[i] / 5));
                return ;
            }

        }

       // если все проверки пройдены отрисовываем тень
        for (int i = 0; i < listOfIndexs.Count; i++)
        {
            x = zeroPoint + 10 * (int)(listOfIndexs[i] / BlockInShape.matrixLength) + listOfIndexs[i] % BlockInShape.matrixLength;
            //меняем прозрачность 
            color.a = alphaLevel;
            FieldManager.field.GetChild(x).GetComponent<Image>().sprite = transform.GetComponent<Image>().sprite;
            FieldManager.field.GetChild(x).GetComponent<Image>().color = color;

        }
    
    }


}
