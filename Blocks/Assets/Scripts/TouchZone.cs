using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchZone : MonoBehaviour
{
    public float padding;

    public FieldManager fieldManager;


    public Color currentColor;

    Vector3 posOfTouch;
    Vector2 startPos;

    bool flag = false;
    public static bool iSinglTouchZone = true;

    private void Start()
    {
        fieldManager = FieldManager.field.transform.GetComponent<FieldManager>();
        //====
        float x = FieldManager.field.GetComponent<GridLayoutGroup>().cellSize.x;
        transform.GetComponent<GridLayoutGroup>().cellSize = new Vector2(x*0.7f,x * 0.7f);


        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().color = ColorManager.GetNextColor();
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
                //Debug.Log(i + " = " + transform.GetChild(i).GetComponent<Image>().enabled)
                

                break;
            }
        }
        padding = Mathf.Abs( transform.GetComponent<RectTransform>().sizeDelta.y ) ;
        //Debug.Log(padding);
        currentColor = ColorManager.GetNextColor();
        ColorManager.IncrementColor();

    }

    private void Update()
    {
        if (flag)
            if (Input.touchCount > 0)
            {
                posOfTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
               
                posOfTouch.z = 0;

                //posOfTouch.y += Mathf.Abs(transform.GetComponent<RectTransform>().sizeDelta.y)/6;
                float x = fieldManager.GetComponent<GridLayoutGroup>().cellSize.x/1.35f;
                posOfTouch.y += x;


                transform.position = new Vector2(posOfTouch.x, posOfTouch.y);
                //transform.localPosition = posOfTouch;

            }
    }



    public void PointerDown()
    {
        if (iSinglTouchZone)
        {
            flag = true;
            startPos = this.transform.position;
            
            transform.localScale = new Vector3( 1.4f, 1.4f);
            iSinglTouchZone = false;
        }
        

    }
    public void PointerUp()
    {
        if (flag)
        {
            CheckZones();
            flag = false;

            this.transform.position = startPos;
            transform.localScale = new Vector3( 1, 1);
            iSinglTouchZone = true;
        }
    }

    void CheckZones()
    {


        if (fieldManager.ChekShapeForPlacement(transform))
            GameObject.Destroy(gameObject);
          

        fieldManager.CheckFieldForFullLines();
        fieldManager.CheckForLoss();
    }


}
