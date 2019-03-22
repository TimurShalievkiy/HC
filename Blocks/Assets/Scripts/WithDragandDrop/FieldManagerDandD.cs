using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldManagerDandD : MonoBehaviour
{

    [SerializeField] Transform[] cellsPosArray;
    [SerializeField] Cell[] cellsStatePosArray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInstance();
    }

    bool CheckForInstance()
    {
        for (int i = 0; i < cellsPosArray.Length; i++)
        {
            if (Vector3.Distance(cellsPosArray[i].position, TouchZoneWithDragAndDrop.currentShapePos) < 20)
            {
                cellsPosArray[i].GetComponent<Image>().color = Color.red;
                Debug.Log(i);
                break;
            }
        }
        return false;
    }
}
