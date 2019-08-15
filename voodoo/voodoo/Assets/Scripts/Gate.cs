using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gate : MonoBehaviour
{
    [SerializeField] BoxCollider[] colliders;
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.transform.name);
    //    if (collision.transform.tag == "jelly")
    //    {
    //        Debug.Log("Jelly");
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "jelly")
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[0].enabled = false;
            }

            Handheld.Vibrate();
        }
    }
}


