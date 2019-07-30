using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShapeView : MonoBehaviour
{

    [SerializeField] private ShapeController controller;


    static bool isSingleShape = true;

    bool IsCurrentShape = false;

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

        //controller.OnSetShapesPositionAfterCreating += SetShapesPosAfterCreating;
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

        controller.OnInitializeShape -= OnInitializedShape;
    }


    private void InitializeShapeBeforeDrag(PointerEventData eventData)
    {
        //ностроить не восприимчевость к мультитачу
        if (isSingleShape)
        {
            controller.startPos = transform.position;
           
            isSingleShape = false;
            IsCurrentShape = true;
        }
    }



    public void OnDragShape(PointerEventData eventData)
    {

        if (IsCurrentShape && eventData.pointerId <= 0)
        {
                transform.position += (Vector3)eventData.delta;
                transform.position = new Vector3(eventData.position.x, eventData.position.y + controller.currentDistance);
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
        if (IsCurrentShape && eventData.pointerId <=0)
        {
            transform.position = controller.startPos;
            isSingleShape = true;
            IsCurrentShape = false;
        }
    }

    public void OnInitializedShape(List<int> listIndexOfBlox)
    {
        controller.firsBlock = controller.listBlockInShape[0].transform;
        bool hasBorder = false; // тут гет с скин менеджера есть ли у данного скина рамка
        foreach (var item in listIndexOfBlox)
        {
            if (hasBorder)
            {
                controller.listBlockInShape[item].enabled = true;

                controller.listBlockInShape[item].transform.GetChild(0).GetComponent<RawImage>().enabled = true;
            }
            else
            {
                controller.listBlockInShape[item].enabled = false;

                controller.listBlockInShape[item].transform.GetChild(0).GetComponent<RawImage>().enabled = true;
                controller.listBlockInShape[item].transform.localScale = new Vector3(1, 1);
            }
            
        }
    }



}
