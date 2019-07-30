using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ShapeController : MonoBehaviour, IDragHandler, IEndDragHandler, IInitializePotentialDragHandler, IBeginDragHandler
{

    public Action<PointerEventData> OnInitializePotentialBeforeDrag;
    public Action<PointerEventData> OnDragShape;
    public Action<PointerEventData> OnEndDragShape;
 
    public Action<List<int>> OnInitializeShape;

   // public Action OnSetShapesPositionAfterCreating;

    public bool isOn;

    public float currenShapeSize;




    Vector3 currentShapePos;

    public Vector2 startPos;

    [SerializeField] float DistanceInCountOfCells = 4f;

    [SerializeField] public float currentDistance;

    public  Transform firsBlock;

    public List<int> listPosActivBlockInShape = new List<int>();

    public List<RawImage> listBlockInShape;


    private void Start()
    {

        currenShapeSize = transform.GetComponent<RectTransform>().rect.width;
        //currentDistance = Screen.width / 10 * DistanceInCountOfCells;
        currentDistance = currenShapeSize ;



        for (int i = 0; i < transform.childCount; i++)
        {
            listBlockInShape.Add(transform.GetChild(i).GetComponent<RawImage>());
        }

        OnInitializeNewShape(ShapesManager.GetListIndexBlockByShapeId(UnityEngine.Random.Range(0,19)));
    }


    public void OnDrag(PointerEventData eventData)
    {
        OnDragShape?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragShape?.Invoke(eventData);
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
            OnInitializePotentialBeforeDrag?.Invoke(eventData);        
    }

    public void OnInitializeNewShape(List<int> listIndexOfBlox)
    {
        OnInitializeShape?.Invoke(listIndexOfBlox);
    }

    public void OnBeginDrag(PointerEventData eventData) {  }
}
