using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Box>() != null)
        {
            if (collision.transform.parent.transform.parent.GetComponent<Claster>() != null)
                collision.transform.parent.transform.parent.GetComponent<Claster>().speedOfClaster = JsonFileWriter.jsondata.speedOfClaster;
            else if (collision.transform.parent.GetComponent<Claster>() != null)
            {
                collision.transform.parent.GetComponent<Claster>().speedOfClaster = JsonFileWriter.jsondata.speedOfClaster;
            }
        }

    }
}
