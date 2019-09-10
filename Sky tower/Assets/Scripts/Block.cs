using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    bool isTouched = false;
    bool detouch = false;
    public Rigidbody2D _rigidbody2d;

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.transform.tag == "startPlace")
        {
            CraneController.instance.CreateBlock();
            isTouched = true;
        }

        if (!isTouched)
            if (collision.transform.tag == "block")
            {
                _rigidbody2d.velocity = Vector2.zero;
                isTouched = true;


                if (transform.localPosition.x <= 0.12 && transform.localPosition.x >= -0.22)
                    Debug.Log("Perfect");

                Debug.Log(222);
                CraneController.instance.CreateBlock();
            }
            

    }

    private void Update()
    {
        if (detouch)
        {
            
            if (transform.rotation.z > 0)
            {
                transform.eulerAngles -= new Vector3(0,0,1f);
                if (Vector3.Distance(transform.eulerAngles, Vector3.zero) <= 1)
                {
                    detouch = false;
                    transform.eulerAngles = Vector3.zero;
                }
            }
            else
            {
                transform.eulerAngles += new Vector3(0, 0, 1f);
                if (Vector3.Distance(transform.eulerAngles, Vector3.zero) <= 1)
                {
                    detouch = false;
                    transform.eulerAngles = Vector3.zero;
                }
                    
            }
                
        }
    }

    public void Detouch()
    {
        transform.parent = null;
        detouch = true;
    }
}
