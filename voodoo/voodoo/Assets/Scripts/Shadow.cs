using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{

    [SerializeField] Transform shadowTarget;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "jelly")
        {

            Jelly.SetNewPos(transform.InverseTransformPoint(shadowTarget.transform.position));
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
       
       
    //}




}
