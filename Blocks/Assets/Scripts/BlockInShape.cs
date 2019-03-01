using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockInShape : MonoBehaviour
{
    public static int matrixLength = 5;
    public FieldManager fieldManager;
  
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

                MakeShapeShadowInGameField();
            }
            else
            {
                transform.parent.GetComponent<TouchZone>().fieldManager.CleerFieldColor();
                target = null;
            }


        }
        else
        {
            transform.parent.GetComponent<TouchZone>().fieldManager.CleerFieldColor();
            target = null;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Cell")
        {
            if (target == null)
            {
                transform.parent.GetComponent<TouchZone>().fieldManager.CleerFieldColor();
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




    public void MakeShapeShadowInGameField()
    {
        transform.parent.GetComponent<TouchZone>().fieldManager.CleerFieldColor();
        int length = BlockInShape.matrixLength;
        int numBoxWithColl = -1;
        int targetIndex = -1;
        Color color = new Color();

        List<int> listOfIndexs = new List<int>();

        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).GetComponent<Image>().enabled)
            {
                listOfIndexs.Add(transform.parent.GetChild(i).GetSiblingIndex());
            }
        }

        numBoxWithColl = transform.GetSiblingIndex();
        targetIndex = target.GetSiblingIndex();
        color = transform.parent.GetComponent<TouchZone>().currentColor;

        if (targetIndex == -1 || numBoxWithColl == -1)
        {
           // transform.parent.GetComponent<TouchZone>().fieldManager.CleerFieldColor();
            //Debug.Log(targetIndex + " " + numBoxWithColl);
            return ;
        }

        int zeroPoint = targetIndex - numBoxWithColl - (BlockInShape.matrixLength * (int)(numBoxWithColl / BlockInShape.matrixLength));
        int x = zeroPoint + 10 * (int)(listOfIndexs[0] / BlockInShape.matrixLength) + listOfIndexs[0] % BlockInShape.matrixLength;


        int line = (x / 10 - listOfIndexs[0] / 5);
        for (int i = 0; i < listOfIndexs.Count; i++)
        {
            x = zeroPoint + 10 * (int)(listOfIndexs[i] / BlockInShape.matrixLength) + listOfIndexs[i] % BlockInShape.matrixLength;
            if (x > 99 || x < 0)
                return ;

            if (FieldManager.field.GetChild(x).GetComponent<Cell>().isSet)
            {
                return ;
            }


            if (line != (x / 10 - listOfIndexs[i] / 5))
            {
                Debug.Log("linr = " + line + " != " + (x / 10 - listOfIndexs[i] / 5));
                return ;
            }

        }

       // transform.parent.GetComponent<TouchZone>().fieldManager.CleerFieldColor();
        for (int i = 0; i < listOfIndexs.Count; i++)
        {
            x = zeroPoint + 10 * (int)(listOfIndexs[i] / BlockInShape.matrixLength) + listOfIndexs[i] % BlockInShape.matrixLength;
            //FieldManager.field.GetChild(x).GetComponent<Cell>().SetValue(true);
            color.a = 0.5f;
            FieldManager.field.GetChild(x).GetComponent<Image>().color = color;

        }
    
    }


}
