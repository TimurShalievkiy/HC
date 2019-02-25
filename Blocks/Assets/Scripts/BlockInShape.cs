using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockInShape : MonoBehaviour
{
    public static int matrixLength = 5;

  
    Transform target;
    int targetIndex = -1;

    public Transform Target { get => target; set => target = value; }

    public int TargetIndex { get {
            if (target != null)
                return target.GetSiblingIndex();
            else
                return -1;
        }
        set => targetIndex = value; }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Cell")
        {
            if (!collision.transform.GetComponent<Cell>().isSet)
            {
                target = collision.transform;
            }           

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Cell")
        {
            //collision.gameObject.GetComponent<Image>().color = ColorManager.GetDefaultColour();

        }
    }

    public void SetValueForCurrentTarger()
    {
        if (Target != null )
        {
            Target.transform.GetComponent<Image>().color = ColorManager.GetNextColor();

            Target.transform.GetComponent<Cell>().SetValue(true);
        }
    }


}
