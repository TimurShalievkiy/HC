using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchZone : MonoBehaviour
{
    public float padding;

    public FieldManager fieldManager;


    //public Color currentColor;

    Vector3 posOfTouch;
    Vector2 startPos;
    public Vector3 offset;


    public float speed;
    bool flag = false;
    public static bool iSinglTouchZone = true;


    public bool isInStartPos = false;
    private void Start()
    {
        offset = new Vector3();
        fieldManager = FieldManager.field.transform.GetComponent<FieldManager>();
        //====
        float x = FieldManager.field.GetComponent<GridLayoutGroup>().cellSize.x;
        transform.GetComponent<GridLayoutGroup>().cellSize = new Vector2(x*0.7f,x * 0.7f);


        for (int i = 0; i < transform.childCount; i++)
        {
            //transform.GetChild(i).GetComponent<Image>().color = ColorManager.GetNextColor();
            transform.GetChild(i).GetComponent<Image>().sprite = ScinManager.GetNextSq();



            if (transform.GetChild(i).GetComponent<BoxCollider2D>().enabled)
            {
                transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
            }
           
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Image>().enabled)
            {
                transform.GetChild(i).GetComponentInChildren<BoxCollider2D>().enabled = true;
                break;
            }
        }
        padding = Mathf.Abs( transform.GetComponent<RectTransform>().sizeDelta.y ) ;
        //currentColor = ColorManager.GetNextColor();
        //ColorManager.IncrementColor();
        ScinManager.IncrementIndexOfCurrentSq();

    }

    private void Update()
    {
        if (flag)
        {
            if (Input.touchCount > 0)
            {
                posOfTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                posOfTouch.z = 0;

                // float x = fieldManager.GetComponent<GridLayoutGroup>().cellSize.x/1.35f;
                // float a = posOfTouch.y + fieldManager.GetComponent<GridLayoutGroup>().cellSize.x / 1.35f;
                //posOfTouch.y += fieldManager.GetComponent<GridLayoutGroup>().cellSize.x / 1.35f;

                speed = transform.parent.GetComponent<TouchZonesCreator>().slider.value;



                //transform.position = Vector3.Lerp(transform.position, new Vector3(posOfTouch.x, GetYPos(), 0f), speed);
                //transform.position = Camera.main.ScreenToWorldPoint(new Vector3( GetXPos(),GetYPos(),0f));

                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.3f, 1.3f), 1f);
                transform.position = Vector3.Lerp(transform.position, (new Vector2(GetXPos() , GetYPos() ))+ new Vector2(offset.x,0f),1f);
                transform.parent.GetComponent<TouchZonesCreator>().slider.transform.GetChild(0).GetComponent<Text>().text = transform.parent.GetComponent<TouchZonesCreator>().slider.value.ToString();

            }
        }
        if (isInStartPos)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, 0.3f);
            if (Mathf.Round(transform.position.x) == Mathf.Round(startPos.x))
            {
                this.transform.position = startPos;
                isInStartPos = false;
                iSinglTouchZone = true;
            }
        }
    }

    public float GetYPos()
    {
        //return Mathf.Lerp( posOfTouch.y, posOfTouch.y + fieldManager.GetComponent<GridLayoutGroup>().cellSize.x / 1.35f,0.7f);
        return  Mathf.Lerp(transform.position.y, posOfTouch.y + fieldManager.GetComponent<GridLayoutGroup>().cellSize.x / 1.35f,0.5f);
    }
    public float GetXPos()
    {
        //return Mathf.Lerp( posOfTouch.y, posOfTouch.y + fieldManager.GetComponent<GridLayoutGroup>().cellSize.x / 1.35f,0.7f);
        return posOfTouch.x;
    }

    public void PointerDown()
    {
        if (iSinglTouchZone)
        {
            //Debug.Log(name);
           // Vector3 z = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
           if(Input.touchCount>0)
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            //Debug.Log(transform.position + " - " + z + " = " + offset);
            flag = true;
            startPos = this.transform.position;
            
            //transform.localScale = new Vector3( 1.4f, 1.4f);
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
            //this.transform.position = startPos;
            transform.localScale = new Vector3( 1, 1);
           
        }
    }

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


}
