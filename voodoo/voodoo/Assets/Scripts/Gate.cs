using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gate : MonoBehaviour
{
    [SerializeField] BoxCollider[] colliders;
    [SerializeField] GameObject triggerCollider;
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.transform.name);
    //    if (collision.transform.tag == "jelly")
    //    {
    //        Debug.Log("Jelly");
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "jelly")
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[0].enabled = false;
            }

            

            if (Jelly.shadowTrigers.Count > 0)
            {
                Jelly.shadowTrigers.RemoveRange(0, 1);
                if (Jelly.shadowTrigers.Count > 0)
                {
                    if (Jelly.shadowTrigers[0] != null)
                    {
                        Shadow s = Jelly.shadowTrigers[0].GetComponent<Shadow>();
                        if (s != null)
                            s.isCurrent = true;
                    }
                }
            }


            GameObject.Destroy(triggerCollider.gameObject);


            Jelly.endOfShadow.gameObject.SetActive(false);
   
            Jelly.endOfShadow.position = new Vector3(10000, 0, 10000);

            Shadow.isFree = true;


        }
    }
}


