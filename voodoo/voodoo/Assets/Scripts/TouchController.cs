using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    bool flag = true;
    Vector3 startPos;
    [SerializeField] Transform box;
    float diference = 0;

    bool buttonDown = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            startPos = Input.mousePosition;
            buttonDown = true;

        }
        if (buttonDown)
        {
            diference = Mathf.Clamp(startPos.y - Input.mousePosition.y,-20,20) * 0.01f;


            Vector3 newCector = new Vector3(Mathf.Clamp(box.localScale.x + diference , 0.3f, 1.8f), Mathf.Clamp(box.localScale.y - diference , 0.3f, 1.8f), 1f);

            box.localScale = newCector;
            //box.localScale -= new Vector3(0, diference*0.01f);
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            startPos = Vector3.zero;
            buttonDown = false;
        }
    }
}
