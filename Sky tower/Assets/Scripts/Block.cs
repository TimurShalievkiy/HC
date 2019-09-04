using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    bool isTouched = false;
    [SerializeField] Rigidbody2D rigidbody2d;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "startPlace")
        {
            isTouched = true;
        }

        if (!isTouched)
        if (collision.transform.tag == "block")
        {
                rigidbody2d.velocity = Vector2.zero;
                isTouched = true;


                if (transform.localPosition.x <= 0.12 && transform.localPosition.x >= -0.22)
                    Debug.Log("Perfect");
        }




        }
}
