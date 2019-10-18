using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke : MonoBehaviour
{
    [SerializeField] bool isTriger = true;
    [SerializeField] GameObject particle;
    private void Start()
    {
        particle = transform.GetChild(0).gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "block" || collision.tag == "startPlace")
        {
            //if(!single)
            if(isTriger)
            particle.SetActive(true);
        }
    }
    public void ResetSmoke()
    {
        particle.SetActive(false);
    }
    public void ActivateSmoke()
    {
        Debug.Log(transform.parent.name + " activ" );
        particle = transform.GetChild(0).gameObject;
        particle.SetActive(true);
    }

}
