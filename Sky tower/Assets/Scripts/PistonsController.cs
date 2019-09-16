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
    // Start is called before the first frame update
    void Start()
    {
        
        countOfPerfectPos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (countOfPerfectPos == 0)
        {

           
            if (CraneController.instance.listOfBlocks.Count > 1)
            {
                leftPiston.position = Vector3.Lerp(leftPiston.position, new Vector2(-9.2f, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].leftDot.position.y), 0.2f);
                rightPiston.position = Vector3.Lerp(rightPiston.position, new Vector2(9.2f, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].rightDot.position.y), 0.2f);
                smokeLeft.ResetSmoke();
                smokeRight.ResetSmoke();
            }
        }
        if (countOfPerfectPos == 1)
        {
            leftPiston.position = Vector3.Lerp(leftPiston.position, new Vector2(-leftDistance , CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].leftDot.position.y),0.2f);
            rightPiston.position =  Vector3.Lerp(rightPiston.position, new Vector2(rightDistance , CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].rightDot.position.y),0.2f);
        }
        if (countOfPerfectPos == 2)
        {
            leftPiston.position = Vector3.Lerp(leftPiston.position, new Vector2(-leftDistance, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].leftDot.position.y), 0.2f);
            rightPiston.position = Vector3.Lerp(rightPiston.position, new Vector2(rightDistance, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].rightDot.position.y), 0.2f);
        }
        if (countOfPerfectPos == 3)
        {
            leftPiston.position = Vector3.Lerp(leftPiston.position, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].leftDot.position, 0.2f);
            rightPiston.position = Vector3.Lerp(rightPiston.position,  CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].rightDot.position, 0.2f);

            for (int i = 0; i < CraneController.instance.listOfBlocks.Count-1; i++)
            {

                CraneController.instance.listOfBlocks[i]._rigidbody2d.isKinematic = true;

            }
            //StartCoroutine(MovePistonsToBlock());
            if (Vector3.Distance(leftPiston.position, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].leftDot.position) <= 0.1&&
                Vector3.Distance(rightPiston.position, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].rightDot.position) <= 0.1)
            {
                countOfPerfectPos = 0;
                Instantiate(leftPiston, leftPiston.position, Quaternion.identity, leftPiston.parent);
                Instantiate(rightPiston, rightPiston.position, Quaternion.identity, rightPiston.parent);

                leftPiston.position = new Vector2(-9.2f, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].leftDot.position.y);
                rightPiston.position = new Vector2(9.2f, CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].rightDot.position.y);

            }


        }
    }

    public void IncrementCountOfPerfect()
    {

        countOfPerfectPos++;
        float y = CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].leftDot.position.y;
        leftDistance = Vector3.Distance(new Vector3( leftPiston.position.x,y), CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].leftDot.position);
        rightDistance = Vector3.Distance(new Vector3(rightPiston.position.x, y), CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].rightDot.position);

        if (countOfPerfectPos == 1)
        {
            
            leftDistance = -leftPiston.position.x - (leftDistance *0.3f);
            rightDistance = rightPiston.position.x - (rightDistance * 0.3f);
            Debug.Log(leftDistance + " ---" );
        }
        if (countOfPerfectPos == 2)
        {
           // leftDistance = 0;
           // rightDistance = 0;
        }
        //if (countOfPerfectPos == 1)
        //{
        //    Debug.Log(countOfPerfectPos);
        //    float x = CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].transform.position.x-3;
        //    float y = CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].transform.position.y;

        //    float x2 = leftPiston.transform.position.x * 0.25f; 
        //   // x2 = x2 - x2 * 0.25f;

        //    leftPiston.transform.position = new Vector2(x2 , y);
        //    rightPiston.transform.position = new Vector2(-x2 , y);
        //}
        //else if (countOfPerfectPos == 2)
        //{
        //    Debug.Log(countOfPerfectPos);
        //    float x = CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].transform.position.x;
        //    float y = CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].transform.position.y;

        //    float x2 = leftPiston.transform.position.x - x;
        //    x2 = x2 - x2 * 0.60f-3;
        //    Debug.Log(x2);
        //    leftPiston.transform.position = new Vector2(x2, y);
        //    rightPiston.transform.position = new Vector2(-x2, y);
        //}
        //else if (countOfPerfectPos == 3)
        //{
        //    Debug.Log(countOfPerfectPos);
        //}

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
