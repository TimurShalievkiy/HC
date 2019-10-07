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

    public static bool isPause = false;

    public List<Block> listOfBlocks;


    public static CraneController instance;


    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    public void Iinit()
    {

        boxCreator = CraneMove.instance.blockCreator.transform;

            instance = this;

        listOfBlocks = new List<Block>();

        GameObject g = Resources.Load<GameObject>("Scins\\" + ScinController.GetScin() + "\\baseBlock");
        Debug.Log(ScinController.GetScin());
        Instantiate(g, boxCreator.position, boxCreator.parent.transform.rotation, boxCreator);
        box = boxCreator.GetChild(0).GetComponent<Block>();
        box.name = CameraController.countOfBlock.ToString();

        listOfBlocks.Add(box);

        if(countOfBlock != null)
        countOfBlock.text = CameraController.countOfBlock.ToString();

       

    }
    

    public void DropBox()
    {
        if(!CheckForLoose.loose && !isPause)
        if (boxCreator.childCount > 0)
        {

            boxCreator.GetChild(0).GetComponent<Rigidbody2D>().simulated = true;
            box.Detouch();
            hasBlock = false;
        }
        
    }

    public void CreateBlock(Rigidbody2D blockForHinge)
    {
  
        if (!hasBlock && boxCreator.childCount==0)
        {

            switch (CameraController.countOfBlock)
            {

                case 8:
                    CameraController.hard = 2;
                    CraneMove.instance.ChangeSpeed(0.15f);
                    CraneMove.instance.ChangeAngle(8);
                    CraneMove.instance.ChangeTopDownLength(0.5f, 0.7f);
                    break;
                case 16:
                    CameraController.hard = 3;
                    CraneMove.instance.ChangeSpeed(0.1f);
                    CraneMove.instance.ChangeAngle(10);
                    CraneMove.instance.ChangeTopDownLength(0.8f, 1f);
                    break;
                case 26:
                    CameraController.hard = 4;
                    CraneMove.instance.ChangeSpeed(0.08f);
                    CraneMove.instance.ChangeAngle(12);
                    CraneMove.instance.ChangeTopDownLength(1f, 1.2f);
                    break;
                case 34:
                    CameraController.hard = 5;
                    CraneMove.instance.ChangeSpeed(0.07f);
                    CraneMove.instance.ChangeAngle(15);
                    CraneMove.instance.ChangeTopDownLength(1.2f, 1.2f);
                    break;

            }
            hasBlock = true;
            GameObject g = Resources.Load<GameObject>("Scins\\" + ScinController.GetScin() + "\\baseBlock");

            
            Sprite[] s = Resources.LoadAll<Sprite>("Scins\\" + ScinController.GetScin() + "\\blocks\\");
            int indexOfSprite = CameraController.hard - 1;
            indexOfSprite = Mathf.Clamp(indexOfSprite, 0, s.Length-1);
           
                
            g.GetComponent<SpriteRenderer>().sprite = s[indexOfSprite];

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
            
            

        }
    }

    public void Pause(bool pause)
    {
        isPause = pause;
    }

    public void StopVelocity()
    {
        for (int i = 0; i < listOfBlocks.Count; i++)
        {

            if (!listOfBlocks[i]._rigidbody2d.isKinematic)
                listOfBlocks[i]._rigidbody2d.velocity = Vector2.zero;
            else
                listOfBlocks[i].StopAllCoroutines();
        }
    }

    public void restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
