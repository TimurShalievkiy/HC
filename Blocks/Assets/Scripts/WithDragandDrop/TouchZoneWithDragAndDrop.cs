using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class TouchZoneWithDragAndDrop : MonoBehaviour, IDragHandler, IEndDragHandler,IInitializePotentialDragHandler
{
    public Action OnButtonPressed;

    List<int> posActivBlockInShape = new List<int>();
    Vector3 currentShapePos;
    [SerializeField] FieldManagerDandD fieldManager;

    Vector2 startPos;
    Vector3 position;
    float offset;
    [Range(1, 10)][SerializeField]  float DistanceInCountOfCells = 1.35f;
    float currentDistance;

    public static Transform firsBlock;


    private void Start()
    {
        //offset =Screen.width/10;
        //currentDistance = offset * DistanceInCountOfCells;
        //startPos = transform.position;

        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    if (transform.GetChild(i).GetComponent<Image>().enabled)
        //    {
        //        firsBlock = transform.GetChild(i).transform;
        //        break;
        //    }
        //}
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    if (transform.GetChild(i).GetComponent<Image>().enabled)
        //    {
        //        posActivBlockInShape.Add(i);
        //    }
        //}
        //firsBlock = transform.GetChild(posActivBlockInShape[0]).transform;
        //// Debug.Log(firsBlock.position);
    }

    
    public void OnDrag(PointerEventData eventData)
    {

#if UNITY_EDITOR
        if (eventData.clickCount == 1)
        {

            transform.position += (Vector3)eventData.delta;
            transform.position = new Vector3(transform.position.x, eventData.position.y + currentDistance);
            currentShapePos = transform.position;

            if (fieldManager.CheckForInstance(posActivBlockInShape))
                fieldManager.CreateShadow();
            else
            {
               // Debug.Log(123);
                fieldManager.ClearFieldFromShadow();
            }
        }
#endif
        if (eventData.pointerId == 0 && Input.touchCount == 1)
        {
           // Debug.Log(2);
            transform.position += (Vector3)eventData.delta;
            transform.position = new Vector3(transform.position.x, eventData.position.y + currentDistance);
            currentShapePos = transform.position;
            if (fieldManager.CheckForInstance(posActivBlockInShape))
                fieldManager.CreateShadow();
            else
            {
                //Debug.Log(123);
                fieldManager.ClearFieldFromShadow();
            }
        }
     
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (Input.touchCount == 1 || eventData.clickCount == 1)
        {
            fieldManager.ClearFieldFromShadow();
            transform.position = startPos;
        }
        
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        OnButtonPressed?.Invoke();

    }


}
