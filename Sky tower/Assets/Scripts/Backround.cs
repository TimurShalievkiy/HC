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
            transform.localScale = new Vector3(28.6f, 28.6f, 28.6f);
        }


        if (x >= 1.7 && x < 2)
        {
            Debug.Log("x >= 1.7 && x < 2");
            transform.localScale = new Vector3(26.61f, 26.61f, 26.61f);
        }


        if (x == 2)
        {
            Debug.Log("x == 2");
            transform.localScale = new Vector3(23.7f, 23.7f, 23.7f);
        }

        if (x > 2)
        {
            Debug.Log("x == 2");
            transform.localScale = new Vector3(23.7f, 23.7f, 23.7f);
        }

        // Debug.Log(rectTransform.sizeDelta);
        Debug.Log(x);
    }
    private void Update()
    {
        float x = (float)Screen.height / Screen.width;

        if (x >= 1.33 && x < 1.666)
        {

            transform.localScale = new Vector3(35.5f, 35.5f, 35.5f);
        }

        if (x >= 1.666 && x < 1.7)
        {
            
            transform.localScale = new Vector3(28.6f, 28.6f, 28.6f);
        }


        if (x >= 1.7 && x < 2)
        {
         
            transform.localScale = new Vector3(26.61f, 26.61f, 26.61f);
        }


        if (x == 2)
        {
            
            transform.localScale = new Vector3(23.7f, 23.7f, 23.7f);
        }

        if (x > 2)
        {
         
            transform.localScale = new Vector3(23.7f, 23.7f, 23.7f);
        }

    }


}
