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
    [SerializeField] Animator animator;
    bool hasBlock = true;

    public static bool isPause = false;

    public List<Block> listOfBlocks;


    public static CraneController instance;


    // Start is called before the first frame update
    public void Iinit()
    {

        boxCreator = CraneMove.instance.blockCreator.transform;

        instance = this;
        animator = GameController.instance.GetCrane().transform.GetComponent<Animator>();
        listOfBlocks = new List<Block>();

        GameObject g = Resources.Load<GameObject>("Scins\\" + ScinController.GetScin() + "\\baseBlock");
        Debug.Log(ScinController.GetScin());
        Instantiate(g, boxCreator.position, boxCreator.parent.transform.rotation, boxCreator);
        box = boxCreator.GetChild(0).GetComponent<Block>();
        box.name = CameraController.countOfBlock.ToString();

        listOfBlocks.Add(box);

        if (countOfBlock != null)
            countOfBlock.text = CameraController.countOfBlock.ToString();



    }


    public void DropBox()
    {
        if (!CheckForLoose.loose && !isPause)
            if (boxCreator.childCount > 0)
            {
                SetAnimationFlag(true);
                boxCreator.GetChild(0).GetComponent<Rigidbody2D>().simulated = true;
                box.Detouch();
                hasBlock = false;
            }

    }

    public void CreateBlock(Rigidbody2D blockForHinge)
    {

        if (!hasBlock && boxCreator.childCount == 0)
        {

            switch (CameraController.countOfBlock)
            {

                case 4:
                    CameraController.hard = 1;
                    CraneMove.instance.ChangeCranHard(0.15f, 8);
                    CraneMove.instance.ChangeTopDownLength(0.5f, 0.7f);
                    break;
                case 9:
                    CameraController.hard = 2;
                    CraneMove.instance.ChangeCranHard(0.1f, 10);
                    CraneMove.instance.ChangeTopDownLength(0.8f, 1f);
                    break;
                case 20:
                    CameraController.hard = 3;
                    CraneMove.instance.ChangeCranHard(0.08f, 12);

                    CraneMove.instance.ChangeTopDownLength(1f, 1.2f);
                    break;
                case 34:
                    CameraController.hard = 5;
                    CraneMove.instance.ChangeCranHard(0.07f, 15);
                    CraneMove.instance.ChangeTopDownLength(1.2f, 1.2f);
                    break;

            }
            hasBlock = true;
            GameObject g = Resources.Load<GameObject>("Scins\\" + ScinController.GetScin() + "\\baseBlock");


            Sprite[] s = Resources.LoadAll<Sprite>("Scins\\" + ScinController.GetScin() + "\\blocks\\");
            int indexOfSprite = CameraController.hard - 1;
            indexOfSprite = Mathf.Clamp(indexOfSprite, 0, s.Length - 1);


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

    public void SetAnimationFlag(bool flag)
    {
        animator.SetBool("PlayAnim", flag);
        if (flag)
            StartCoroutine(WhaitAnim());
    }

    public void SetAnimationFlagOnFalse()
    {
        animator.SetBool("PlayAnim", false);
    }

    IEnumerator WhaitAnim()
    {
        yield return new WaitForSeconds(0.4f);
        SetAnimationFlagOnFalse();
    }
}
