using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchZone : MonoBehaviour
{


    public FieldManager fieldManager;


    Vector3 posOfTouch;
    Vector2 startPos;
    public Vector3 offset;

    Camera camera;



    bool flag = false;
    public static bool iSinglTouchZone = true;


    public bool isInStartPos = false;

    float distansToShape;
    float duration = 0.3f;
    float currentDuration ;
    private void Start()
    {
        currentDuration = duration;
        offset = new Vector3();
        InitShape();
        camera = Camera.main;
        distansToShape = fieldManager.GetComponent<GridLayoutGroup>().cellSize.x / 1.35f;

    }
    private void FixedUpdate()
    {
        if (flag)
        {
            if (Input.touchCount > 0)
            {
                posOfTouch = camera.ScreenToWorldPoint(Input.GetTouch(0).position);

                posOfTouch.z = 0;
        
                //transform.position = Vector3.Lerp(transform.position, new Vector3(posOfTouch.x, GetYPos(), 0f), speed);
                //transform.position = Camera.main.ScreenToWorldPoint(new Vector3( GetXPos(),GetYPos(),0f));

                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.3f, 1.3f), 1f);
                //transform.position = Vector3.Lerp(transform.position, (new Vector2(GetXPos() , GetYPos() ))+ new Vector2(offset.x,0f),1f);

                posOfTouch.y = GetYPos();
                //posOfTouch.y += fieldManager.GetComponent<GridLayoutGroup>().cellSize.x / 1.35f;
                transform.position = posOfTouch + new Vector3(offset.x, 0f);

                //transform.parent.GetComponent<TouchZonesCreator>().slider.transform.GetChild(0).GetComponent<Text>().text = transform.parent.GetComponent<TouchZonesCreator>().slider.value.ToString();

            }
        }
        //если не в стартовой позиции
        if (isInStartPos)
        {
            //возвращаем фигуру в стартовую позицию
            transform.position = Vector3.Lerp(transform.position, startPos, 0.3f);

            //если достигли стартовой позиции
            if (Mathf.Round(transform.position.x) == Mathf.Round(startPos.x))
            {
                //выставляем параметры в базовое значение
                this.transform.position = startPos;
                currentDuration = duration;
                isInStartPos = false;
                iSinglTouchZone = true;
            }
        }
    }

    //получить Y позицию с поднятием фигуры по таймеру на высоту **********высоту сделать настраиваемой после оптимизации скорости
    public float GetYPos()
    {
        if (currentDuration >= 0)
        {
            currentDuration -= Time.deltaTime;
            return Mathf.Lerp(transform.position.y, posOfTouch.y + distansToShape, 0.5f);
        }
        //return Mathf.Lerp( posOfTouch.y, posOfTouch.y + fieldManager.GetComponent<GridLayoutGroup>().cellSize.x / 1.35f,0.7f);
        return posOfTouch.y + distansToShape; //Mathf.Lerp(transform.position.y, posOfTouch.y + fieldManager.GetComponent<GridLayoutGroup>().cellSize.x / 1.35f,0.5f);
    }

    //получить х позицию тача
    public float GetXPos()
    {
        //return Mathf.Lerp( posOfTouch.y, posOfTouch.y + fieldManager.GetComponent<GridLayoutGroup>().cellSize.x / 1.35f,0.7f);
        return posOfTouch.x;
    }

    public void PointerDown()
    {
        //если больше не задействовано других зон
        if (iSinglTouchZone)
        {
            //назначаем офсет для просчета разницы между тачем и центром фигуры
           if(Input.touchCount>0)
                offset = transform.position - Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            flag = true;
            startPos = this.transform.position;
            
            iSinglTouchZone = false;
        }
        

    }
    public void PointerUp()
    {
        if (flag)
        {
            CheckZones();
            flag = false;
            isInStartPos = true;
            transform.localScale = new Vector3( 1, 1);         
        }
    }

    //проверка на возможность разместить фигуру
    void CheckZones()
    {


        if (fieldManager.ChekShapeForPlacement(transform))
        {
            iSinglTouchZone = true;
            GameObject.Destroy(gameObject);
        }
          

        fieldManager.CheckFieldForFullLines();
        fieldManager.CheckForLoss();
    }



    void InitShape()
    {
        //получаем ссылку на FieldManager
        fieldManager = FieldManager.field.transform.GetComponent<FieldManager>();

        //задаем стартовую позицию
        startPos = this.transform.position;

        //назначаем размер ячейки
        float x = FieldManager.field.GetComponent<GridLayoutGroup>().cellSize.x;

        //высталяем параметр нахождения в стартовой позиции в тру для избежания богов с невозвратом фигуры 
        isInStartPos = true;

        //задаем размер блоков фигуры
        transform.GetComponent<GridLayoutGroup>().cellSize = new Vector2(x * 0.7f, x * 0.7f);


        //проводим базовую инициализацию фигуры
        for (int i = 0; i < transform.childCount; i++)
        {
            //задаем спрайт
            transform.GetChild(i).GetComponent<Image>().sprite = ScinManager.GetNextSq();

            //деактивируем ненужные BoxCollider2D
            if (transform.GetChild(i).GetComponent<BoxCollider2D>().enabled)
            {
                transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
            }
        }


        //активируем нужный BoxCollider2D в обьекте где активен Image
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Image>().enabled)
            {
                transform.GetChild(i).GetComponentInChildren<BoxCollider2D>().enabled = true;
                transform.GetChild(i).GetComponentInChildren<BoxCollider2D>().size = new Vector2(2, 2);
                break;
            }
        }


        ScinManager.IncrementIndexOfCurrentSq();
    }

}
