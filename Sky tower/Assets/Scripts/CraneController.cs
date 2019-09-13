using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CraneController : MonoBehaviour
{
    public PistonsController pistonsController;

    [SerializeField] Transform boxCreator;
    Block box;
    [SerializeField] Text countOfBlock;
    bool hasBlock = true;

    public List<Block> listOfBlocks;


    public static CraneController instance;
    // Start is called before the first frame update
    void Start()
    {
        if (CraneController.instance == null)
            instance = this;

        listOfBlocks = new List<Block>();

        GameObject g = Resources.Load<GameObject>("Block");
        Instantiate(g, boxCreator.position, boxCreator.parent.transform.rotation, boxCreator);
        box = boxCreator.GetChild(0).GetComponent<Block>();
        box.name = CameraController.countOfBlock.ToString();

        listOfBlocks.Add(box);

        countOfBlock.text = CameraController.countOfBlock.ToString();

    }


    public void DropBox()
    {
        if(!CheckForLoose.loose)
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


           

            listOfBlocks.Add(box);

            for (int i = 0; i < listOfBlocks.Count; i++)
            {


                listOfBlocks[i]._rigidbody2d.velocity = Vector2.zero;
                
            }
            if (CameraController.countOfBlock == 10)
            {
                CraneMove.instance.ChangeSpeed(0.1f);
            }

        }
    }

    public void StopVelocity()
    {
        for (int i = 0; i < listOfBlocks.Count; i++)
        {


            listOfBlocks[i]._rigidbody2d.velocity = Vector2.zero;
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
