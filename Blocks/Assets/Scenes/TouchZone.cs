using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchZone : MonoBehaviour
{
    Vector3 posOfTouch;
    Vector2 startPos;
    
    bool flag = false;
    private void Update()
    {
        if(flag)
        if (Input.touchCount > 0)
        {
            posOfTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position); 
            posOfTouch.z = 0;
            posOfTouch.y += 30;
            //Debug.Log(posOfTouch);
            this.transform.position = posOfTouch;
        }
    }
    public void PointerDown()
    {
        flag = true;
        startPos = this.transform.position;
       
    }
    public void PointerUp()
    {
        CheckZones();
        flag = false;
        this.transform.position = startPos;
        
    }

    void CheckZones()
    {
        for (int i = 0; i < transform.childCount; i++)
        {

            transform.GetChild(i).GetComponent<BlockInShape>().GetCurrentTarger();
        }
        ColorManager.IncrementColor();        
        Debug.Log(1);
    }
}
