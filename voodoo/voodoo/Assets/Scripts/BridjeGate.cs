using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridjeGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "jelly")
        {
 
           // Handheld.Vibrate();


        }
    }
}
