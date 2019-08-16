using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
   
    bool needToGrow = true;
    public static bool isFree = true;
    public bool isCurrent = false;
    [SerializeField] Transform shadowTarget;
    [SerializeField] MeshRenderer render;

    private void OnTriggerStay(Collider other)
    {

        if (other.transform.tag == "jelly")
        {

            if (!Jelly.shadowTrigers.Exists(x => x == transform))
            {
                Jelly.shadowTrigers.Add(transform);
              

            }

            if (isFree || isCurrent)
            {

                if (Jelly.shadowTrigers.Count > 0 && Jelly.shadowTrigers[0] == transform)
                {
                    isFree = false;
                    isCurrent = true;
                    if (!needToGrow)
                    {
                        

                        render.enabled = true;

                        Jelly.SetNewPos(shadowTarget.transform.position, isCurrent);
                        transform.parent.localScale -= new Vector3(0, 0, MoveController.currentSpeed * Time.deltaTime * 10);
  
                    }
                    else
                    {
                        transform.parent.localScale -= new Vector3(0, 0, 10);
                    }
                }

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "jelly" && isCurrent)
        {
            needToGrow = false;
        }
    }


    private void Update()
    {
        if ( MoveController.isPush && Jelly.shadowTrigers.Count > 0 && Jelly.shadowTrigers[0] == transform && isCurrent)
        {
          
            Jelly.shadowTrigers.RemoveRange(0, 1);
            if (Jelly.shadowTrigers.Count > 0)
            {
                Jelly.shadowTrigers.RemoveRange(0, 1);
        
            }
            Jelly.endOfShadow.gameObject.SetActive(false);
            Destroy(transform.parent.gameObject);
        }
    }




}
