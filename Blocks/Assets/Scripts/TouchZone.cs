using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchZone : MonoBehaviour
{
    public float padding;

    public FieldManager fieldManager;


    Vector3 posOfTouch;
    Vector2 startPos;
    public Vector3 offset;


    public float speed;
    bool flag = false;
    public static bool iSinglTouchZone = true;


    public bool isInStartPos = false;

    float duration = 0.3f;
    float currentDuration ;
    private void Start()
    {
        currentDuration = duration;
        offset = new Vector3();
        IniTShape();

    }
    private void FixedUpdate()
    {
        if (flag)
        {
            if (Input.touchCount > 0)
            {
                posOfTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                posOfTouch.z = 0;

                speed = transform.parent.GetComponent<TouchZonesCreator>().slider.value;



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
        if (isInStartPos)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, 0.3f);
            if (Mathf.Round(transform.position.x) == Mathf.Round(startPos.x))
            {
                this.transform.position = startPos;
                currentDuration = duration;
                isInStartPos = false;
                iSinglTouchZone = true;
            }
        }
    }


    public float GetYPos()
    {
        if (currentDuration >= 0)
        {
            currentDuration -= Time.deltaTime;
            return Mathf.Lerp(transform.position.y, posOfTouch.y + fieldManager.GetComponent<GridLayoutGroup>().cellSize.x / 1.35f, 0.5f);
        }
        //return Mathf.Lerp( posOfTouch.y, posOfTouch.y + fieldManager.GetComponent<GridLayoutGroup>().cellSize.x / 1.35f,0.7f);
        return posOfTouch.y + fieldManager.GetComponent<GridLayoutGroup>().cellSize.x / 1.35f; //Mathf.Lerp(transform.position.y, posOfTouch.y + fieldManager.GetComponent<GridLayoutGroup>().cellSize.x / 1.35f,0.5f);
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



    void IniTShape()
    {
        
        fieldManager = FieldManager.field.transform.GetComponent<FieldManager>();
        startPos = this.transform.position;
        float x = FieldManager.field.GetComponent<GridLayoutGroup>().cellSize.x;
        isInStartPos = true;

        transform.GetComponent<GridLayoutGroup>().cellSize = new Vector2(x * 0.7f, x * 0.7f);

        for (int i = 0; i < transform.childCount; i++)
        {
  
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
                transform.GetChild(i).GetComponentInChildren<BoxCollider2D>().size = new Vector2(2, 2);
                break;
            }
        }
        padding = Mathf.Abs(transform.GetComponent<RectTransform>().sizeDelta.y);
        ScinManager.IncrementIndexOfCurrentSq();
    }

}
