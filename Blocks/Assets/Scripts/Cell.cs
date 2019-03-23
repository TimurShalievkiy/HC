using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector3 cellPosition;
    //заполнена ли ячейка
    public bool isSet = false;

    //задать значение переменной isSet
    public void SetValue(bool value)
    {
        isSet = value;
    }

    public void InitializeCellPositionValue()
    {
        cellPosition = transform.position;
    }
}
