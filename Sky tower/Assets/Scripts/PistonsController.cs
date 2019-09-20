using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonsController : MonoBehaviour
{
    int countOfPerfectPos = 0;
    [SerializeField] Transform leftPiston;
    [SerializeField] Transform rightPiston;
    [SerializeField] smoke smokeLeft;
    [SerializeField] smoke smokeRight;

    float leftDistance;
    float rightDistance;
    float cameraHeight;
    // Start is called before the first frame update
    void Start()
    {
        
        countOfPerfectPos = 0;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); 
        cameraHeight = min.x-0.5f;


    }

    // Update is called once per frame
    void Update()
    {
        
        if (countOfPerfectPos == 0)
        {

           
            if (CraneController.instance.listOfBlocks.Count > 1)
            {
                Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
                cameraHeight = min.x - 0.5f;

                leftPiston.position = Vector3.Lerp(leftPiston.position, new Vector2(cameraHeight, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].leftDot.position.y), 0.2f);
                rightPiston.position = Vector3.Lerp(rightPiston.position, new Vector2(-cameraHeight, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].rightDot.position.y), 0.2f);

                smokeLeft.ResetSmoke();
                smokeRight.ResetSmoke();
            }
        }
        if (countOfPerfectPos == 1)
        {
            leftPiston.position = Vector3.Lerp(leftPiston.position, new Vector2(leftDistance , CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].leftDot.position.y),0.2f);
            rightPiston.position =  Vector3.Lerp(rightPiston.position, new Vector2(rightDistance , CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].rightDot.position.y),0.2f);
        }
        if (countOfPerfectPos == 2)
        {
            leftPiston.position = Vector3.Lerp(leftPiston.position, new Vector2(leftDistance, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].leftDot.position.y), 0.2f);
            rightPiston.position = Vector3.Lerp(rightPiston.position, new Vector2(rightDistance, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].rightDot.position.y), 0.2f);
        }
        if (countOfPerfectPos == 3)
        {
            leftPiston.position = Vector3.Lerp(leftPiston.position, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].leftDot.position, 0.2f);
            rightPiston.position = Vector3.Lerp(rightPiston.position,  CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].rightDot.position, 0.2f);

            for (int i = 0; i < CraneController.instance.listOfBlocks.Count-1; i++)
            {

                CraneController.instance.listOfBlocks[i]._rigidbody2d.isKinematic = true;
                CraneController.instance.listOfBlocks[i]._rigidbody2d.freezeRotation = true;

            }
            //StartCoroutine(MovePistonsToBlock());
            if (Vector3.Distance(leftPiston.position, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].leftDot.position) <= 0.1&&
                Vector3.Distance(rightPiston.position, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].rightDot.position) <= 0.1)
            {
                countOfPerfectPos = 0;
                Instantiate(leftPiston, leftPiston.position, Quaternion.identity, leftPiston.parent);
                Instantiate(rightPiston, rightPiston.position, Quaternion.identity, rightPiston.parent);

                leftPiston.position = new Vector2(cameraHeight, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].leftDot.position.y);
                rightPiston.position = new Vector2(-cameraHeight, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].rightDot.position.y);

            }


        }
    }

    public void IncrementCountOfPerfect()
    {

        countOfPerfectPos++;
        float y = CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].leftDot.position.y;
        leftDistance = Vector3.Distance(new Vector3(cameraHeight, y), CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].leftDot.position);
        rightDistance = Vector3.Distance(new Vector3(-cameraHeight, y), CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].rightDot.position);

        if (countOfPerfectPos == 1)
        {
            
            leftDistance = cameraHeight + (leftDistance *0.3f);
            rightDistance = -cameraHeight - (rightDistance * 0.3f);

        }
        if (countOfPerfectPos == 2)
        {
            leftDistance = cameraHeight + (leftDistance * 0.6f);
            rightDistance = -cameraHeight - (rightDistance * 0.6f);
        }


    }


    public void ResetCountOfPerfect()
    {
        countOfPerfectPos = 0;
    }
    IEnumerator MovePistonsToBlock()
    {



        yield return new WaitForSeconds(0.25f);
    }
}
