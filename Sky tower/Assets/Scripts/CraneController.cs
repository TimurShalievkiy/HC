using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CraneController : MonoBehaviour
{
    [SerializeField] Transform boxCreator;
    [SerializeField] Block box;
    [SerializeField] Text countOfBlock;
    bool hasBlock = true;

    public List<Block> rigidbody2Ds;


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
        box.name = CameraController.countOfBlock.ToString();

        countOfBlock.text = CameraController.countOfBlock.ToString();

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
    public void CreateBlock(Rigidbody2D blockForHinge)
    {
  
        if (!hasBlock)
        {
            hasBlock = true;
            GameObject g = Resources.Load<GameObject>("Block");
            Instantiate(g, boxCreator.position, boxCreator.parent.transform.rotation, boxCreator);
            box = boxCreator.GetChild(0).GetComponent<Block>();
            
            CameraController.countOfBlock++;
            countOfBlock.text = CameraController.countOfBlock.ToString();

            box.name = CameraController.countOfBlock.ToString();
            box._rigidbody2d = box.GetComponent<Rigidbody2D>();
            box.hinge = box.GetComponent<HingeJoint2D>();
            Debug.Log("1 - " + blockForHinge.name);

           

            rigidbody2Ds.Add(box);

            for (int i = 0; i < rigidbody2Ds.Count; i++)
            {


                rigidbody2Ds[i]._rigidbody2d.velocity = Vector2.zero;
                
            }
            if (CameraController.countOfBlock == 10)
            {
                CraneMove.instance.ChangeSpeed(0.1f);
            }

        }
    }
    public void StopVelocity()
    {
        for (int i = 0; i < rigidbody2Ds.Count; i++)
        {


            rigidbody2Ds[i]._rigidbody2d.velocity = Vector2.zero;
        }
    }

    public void restart()
    {
        SceneManager.LoadScene(1);
    }
    public void Play()
    {
        SceneManager.LoadScene(0);
    }
}
