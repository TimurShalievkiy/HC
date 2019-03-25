using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShapeView : MonoBehaviour
{

    [SerializeField] private ShapeController controller;

    bool IsFree = true;

    private void OnEnable()
    {
        if (controller == null)
        {
            controller = GetComponentInParent<ShapeController>();

        }
        if (controller == null)
        {
            Debug.LogWarning("ShapeView.OnEnable() controller == null");
            return;
        }
        controller.OnInitializePotentialBeforeDrag += InitializeShapeBeforeDrag;
        controller.OnDragShape += OnDragShape;
        controller.OnEndDragShape += OnEndDragShape;

        controller.OnInitializeShape += OnInitializedShape;
    }

    private void OnDisable()
    {
        if (controller == null)
        {
            Debug.LogWarning("ShapeView.OnEnable() controller == null");
            return;
        }
        controller.OnInitializePotentialBeforeDrag -= InitializeShapeBeforeDrag;
        controller.OnDragShape -= OnDragShape;
        controller.OnEndDragShape -= OnEndDragShape;
    }


    private void InitializeShapeBeforeDrag(PointerEventData eventData)
    {
        //ностроить не восприимчевость к мультитачу

        controller.startPos = transform.position;

    }



    public void OnDragShape(PointerEventData eventData)
    {
        if (eventData.pointerId == 0)
        {
            transform.position = Input.GetTouch(eventData.pointerId).position;
            transform.position = new Vector3(transform.position.x, transform.position.y + controller.currentDistance);
        }

        if (Input.GetMouseButton(0))
        {
            transform.position = Input.mousePosition;
            transform.position = new Vector3(transform.position.x, transform.position.y + controller.currentDistance);
        }

        //            if (fieldManager.CheckForInstance(posActivBlockInShape))
        //                fieldManager.CreateShadow();
        //            else
        //            {
        //                fieldManager.ClearFieldFromShadow();
        //            }
        //        }
    }

    public void OnEndDragShape(PointerEventData eventData)
    {
        transform.position = controller.startPos;
        IsFree = true;
    }

    public void OnInitializedShape(List<int> listIndexOfBlox)
    {
        foreach (var item in listIndexOfBlox)
        {
            controller.listBlockInShape[item].transform.GetChild(0).gameObject.SetActive(true);
        }
    }

}
