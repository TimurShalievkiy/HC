using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchZone : MonoBehaviour
{
    public FieldManager fieldManager;




    Vector3 posOfTouch;
    Vector2 startPos;

    bool flag = false;

    private void Start()
    {
        
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                transform.GetChild(i).GetComponent<Image>().color = ColorManager.GetNextColor();

                transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
            }
           
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                transform.GetComponentInChildren<BoxCollider2D>().enabled = true;
                break;
            }
        }

        ColorManager.IncrementColor();

    }

    private void Update()
    {
        if (flag)
            if (Input.touchCount > 0)
            {
                posOfTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                posOfTouch.z = 0;
                posOfTouch.y += 30;
                this.transform.position = posOfTouch;
            }
    }



    public void PointerDown()
    {
        flag = true;
        startPos = this.transform.position;
        this.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);


    }
    public void PointerUp()
    {
        CheckZones();
        flag = false;

        this.transform.position = startPos;
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    void CheckZones()
    {
        List<int> listOfIndexs = new List<int>();
        int numBoxWithColl = -1;
        int targetIndex = -1;
        for (int i = 0; i < transform.childCount; i++)
        {
            //transform.GetChild(i).GetComponent<BlockInShape>().SetValueForCurrentTarger();
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                if (transform.GetChild(i).gameObject.GetComponent<BoxCollider2D>().isActiveAndEnabled)
                {
                    numBoxWithColl = transform.GetChild(i).GetSiblingIndex();

                    targetIndex = transform.GetChild(i).GetComponent<BlockInShape>().TargetIndex;
                }
                listOfIndexs.Add(transform.GetChild(i).GetSiblingIndex());
            }
        }

        //fieldManager.CheckShapeForPlacement(targetIndex, numBoxWithColl, listOfIndexs);


        fieldManager.MakeShapeShadowInGameField(transform);


        for (int i = 0; i < transform.childCount; i++)
        {
            //transform.GetChild(i).GetComponent<BlockInShape>().SetValueForCurrentTarger();
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                transform.GetChild(i).GetComponent<Image>().color = ColorManager.GetNextColor();
            }
        }


        fieldManager.CheckFieldForFullLines();
    }


}
