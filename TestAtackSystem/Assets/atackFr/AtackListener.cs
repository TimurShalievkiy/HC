using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackListener : MonoBehaviour
{
    AtackListener instance;
    public delegate void IsAtaked();
    public  event IsAtaked OnAtacked;

    private void Awake()
    {
        instance = this;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.tag == "Weapon")
        {
            if (PlayerAtackController.isAtacking)
            {
                PlayerAtackController.isAtacking = false;

                if (OnAtacked != null)
                    OnAtacked();

                Debug.Log("is ataked");
            }
        }
    }
}
