using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke : MonoBehaviour
{
    [SerializeField] GameObject particle;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "block"|| collision.tag == "startPlace")
        {
            particle.SetActive(true);
        }
    }
}
