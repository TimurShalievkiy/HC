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
    public int TargetIndex { get => targetIndex; set => targetIndex = value; }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Cell")
        {
            if (!collision.transform.GetComponent<Cell>().isSet)
            {
                Target = collision.transform;
                targetIndex = collision.transform.GetSiblingIndex();
            }           

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Cell")
        {
            if (Target != null)
            {
                targetIndex = -1;
                Target = null;
            }
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


    //public int GetTargetIndex()
    //{
    //    return targetIndex;
    //}

}
