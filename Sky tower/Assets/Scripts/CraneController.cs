using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneController : MonoBehaviour
{
    [SerializeField] Transform boxCreator;
    [SerializeField] Block box;
    bool hasBlock = true;

    List<Block> rigidbody2Ds;


    public static CraneController instance;
    // Start is called before the first frame update
    void Start()
    {
        if (CraneController.instance == null)
            instance = this;

        rigidbody2Ds = new List<Block>();

        GameObject g = Resources.Load<GameObject>("Block");
        Instantiate(g, boxCreator.position, boxCreator.parent.transform.rotation, boxCreator);
        box = boxCreator.GetChild(0).GetComponent<Block>();

    }


    public void DropBox()
    {
        if (boxCreator.childCount > 0)
        {
            boxCreator.GetChild(0).GetComponent<Rigidbody2D>().simulated = true;
            box.Detouch();
            hasBlock = false;
        }
    }
    public void CreateBlock()
    {
        Debug.Log(hasBlock);
        if (!hasBlock)
        {
            hasBlock = true;
            GameObject g = Resources.Load<GameObject>("Block");
            Instantiate(g, boxCreator.position, boxCreator.parent.transform.rotation, boxCreator);
            box = boxCreator.GetChild(0).GetComponent<Block>();
            
            CameraController.countOfBlock++;
            box.name = CameraController.countOfBlock.ToString();

            rigidbody2Ds.Add(box);

            for (int i = 0; i < rigidbody2Ds.Count; i++)
            {

                Debug.Log(rigidbody2Ds[i]._rigidbody2d);
               // Debug.Log(rigidbody2Ds[i]);// = Vector2.zero;
            }

        }
    }
}
