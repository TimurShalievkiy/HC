using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    float time = 0;
    // Update is called once per frame
    void Update()
    {

        transform.position += transform.forward * 20f * Time.deltaTime;
        if (time >=2f)
        {
            Destroy(gameObject);
           
        }
        time += Time.deltaTime;
    }
}
