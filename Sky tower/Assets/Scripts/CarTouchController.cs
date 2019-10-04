using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarTouchController : MonoBehaviour
{

    private void Start()
    {
        gameObject.AddComponent<BoxCollider2D>();
        
    }
    private void OnMouseEnter()
    {
        Click();
    }
    private void OnMouseDown()
    {
        Click();
    }
    // Start is called before the first frame update
    void Click()
    {
        Debug.Log(1111111);
    }
}
