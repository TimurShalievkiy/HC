using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backround : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] float x;
    [SerializeField] float y;
    // Start is called before the first frame update
    void Start()
    {
        float x = (float)Screen.height / Screen.width;

        if (x >= 1.666 && x < 1.7)
        {
            Debug.Log("x >= 1.666 && x < 1.7");
            transform.localScale = new Vector3(2.65f, 2.65f, 2.65f);
        }


        if (x >= 1.7 && x < 2)
        {
            Debug.Log("x >= 1.7 && x < 2");
            transform.localScale = new Vector3(2.47f, 2.47f, 2.47f);
        }


        if (x == 2)
        {
            Debug.Log("x == 2");
            transform.localScale = new Vector3(2.19f, 2.19f, 2.19f);
        }

        if (x > 2)
        {
            Debug.Log("x == 2");
            transform.localScale = new Vector3(2.19f, 2.19f, 2.19f);
        }

        // Debug.Log(rectTransform.sizeDelta);
        Debug.Log(x);
    }
    private void Update()
    {
        float x = (float)Screen.height / Screen.width;

        if (x >= 1.666 && x < 1.7)
        {
 
            transform.localScale = new Vector3(2.65f, 2.65f, 2.65f);
        }


        if (x >= 1.7 && x < 2)
        {
          
            transform.localScale = new Vector3(2.47f, 2.47f, 2.47f);
        }


        if (x == 2)
        {

            transform.localScale = new Vector3(2.19f, 2.19f, 2.19f);
        }

        if (x > 2)
        {
   
            transform.localScale = new Vector3(2.19f, 2.19f, 2.19f);
        }

    }


}
