using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isSet = false;

    public void SetValue(bool value)
    {
        isSet = value;
    }
}
