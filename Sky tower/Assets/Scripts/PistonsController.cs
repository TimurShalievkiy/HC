using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonsController : MonoBehaviour
{
    int countOfPerfectPos = 0;
    [SerializeField] GameObject leftPiston;
    [SerializeField] GameObject rightPiston;
    // Start is called before the first frame update
    void Start()
    {
        countOfPerfectPos = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void IncrementCountOfPerfect()
    {
        countOfPerfectPos++;
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


        if (countOfPerfectPos == 3)
        {

            for (int i = 0; i < CraneController.instance.listOfBlocks.Count; i++)
            {

                CraneController.instance.listOfBlocks[i]._rigidbody2d.isKinematic = true;
 
            }
            //StartCoroutine(MovePistonsToBlock());
            countOfPerfectPos = 0;
        }
        
    }

    IEnumerator MovePistonsToBlock()
    {
        


        yield return new WaitForSeconds(0.25f) ;
    }
}
