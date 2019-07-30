using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    //заполнена ли ячейка
    public bool isSet = false;

    //задать значение переменной isSet
    public void SetValue(bool value)
    {
        isSet = value;
    }

    public Vector3 GetCerrentPosition() { return transform.position; }
}
