using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deadzone : MonoBehaviour
{
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.transform.name);
    //    if (collision.transform.name == "Jelly")
    //        Debug.Log(123);
    //}
    private void OnTriggerEnter(Collider collision)
    {
 
        if (collision.transform.tag == "jelly")
            SceneManager.LoadScene("3");
    }
}
