using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ShapeController : MonoBehaviour, IDragHandler, IEndDragHandler, IInitializePotentialDragHandler
{

    public Action<PointerEventData> OnInitializePotentialBeforeDrag;
    public Action<PointerEventData> OnDragShape;
    public Action<PointerEventData> OnEndDragShape;
    public bool isOn;
    public Action<List<int>> OnInitializeShape;





    Vector3 currentShapePos;

    public Vector2 startPos;

    [SerializeField] float DistanceInCountOfCells = 4f;

    public float currentDistance;

    public static Transform firsBlock;

    public List<int> listPosActivBlockInShape = new List<int>();

    public List<GameObject> listBlockInShape;

    private void Start()
    {
        currentDistance = Screen.width / 10 * DistanceInCountOfCells;

        for (int i = 0; i < transform.childCount; i++)
        {
            listBlockInShape.Add(transform.GetChild(i).gameObject);
        }

        OnInitializeNewShape(new List<int>() { 1,2,3});
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
    
}
