using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldManagerDandD : MonoBehaviour
{

    //[SerializeField] Transform[] cellsPosArray;
    [SerializeField] Cell[] cellsStatePosArray;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void CheckFieldState()
    {
        for (int i = 0; i < cellsStatePosArray.Length; i++)
        {
            cellsStatePosArray[i].InitializeCellPositionValue();
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }

    public bool CheckForInstance()
    {
        for (int i = 0; i < cellsStatePosArray.Length; i++)
        {
            if (Vector3.Distance(cellsStatePosArray[i].cellPosition, TouchZoneWithDragAndDrop.currentShapePos) < 10)
            {
                cellsStatePosArray[i].transform.GetComponent<Image>().color = Color.red;
                Debug.Log(i);
                break;
            }
        }
        return false;
    }
}
