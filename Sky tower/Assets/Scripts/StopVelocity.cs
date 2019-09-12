using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopVelocity : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "startPlace" || collision.tag == "block")
        {
            CraneController.instance.StopVelocity();
        }
    }
}
