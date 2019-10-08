using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //поведение стрелы, полет и удаление
    float time = 0;

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
