using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneClaster : MonoBehaviour {
    
    bool isball = false;
    bool isrocket = false;


    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<Ball>() != null)
        {
            isball = false;
        }
        if (other.transform.GetComponent<RocketController>() != null)
        {
            isrocket = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.GetComponent<RocketController>() != null)
        {
            //то меняем флаг
            isrocket = true;
        }
        if (other.transform.GetComponent<Ball>() != null)
        {
            isball = true;
        }

        if (isball && isrocket)
        {
            ClasterCreator.counterOfClasters = 0;
            Destroy(this.transform.parent.gameObject);

        }
    }
}
