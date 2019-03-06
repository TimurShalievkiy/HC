using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float timer = 1;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            transform.Rotate(Vector3.left, +5000 * Time.deltaTime);
            timer -= Time.deltaTime;
        }
        else
            transform.localRotation = new Quaternion(0, 0, 0, 0);
    }
}
