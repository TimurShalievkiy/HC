using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockInShape : MonoBehaviour
{
    Transform target;

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log(2);
        if (collision.gameObject.name == "Cell")
        {
           
           // Debug.Log(collision.transform.GetSiblingIndex());
           // Debug.Log(collision.transform.GetSiblingIndex());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log(2);
        if (collision.gameObject.name == "Cell")
        {
            target = collision.transform;
           
        }
    }

    public void GetCurrentTarger()
    {
        target.transform.GetComponent<Image>().color = ColorManager.GetNextColor();
    }
}
