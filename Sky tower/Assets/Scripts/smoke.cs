using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke : MonoBehaviour
{
    [SerializeField] bool single = true;
    [SerializeField] GameObject particle;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "block"|| collision.tag == "startPlace")
        {
            //if(!single)

            particle.SetActive(true);
        }
    }
    public void ResetSmoke()
    {
        particle.SetActive(false);
    }

}
