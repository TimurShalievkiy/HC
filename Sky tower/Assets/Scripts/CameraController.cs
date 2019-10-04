using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //[SerializeField] float distanceToBlock;
    [SerializeField] Transform Crane;
    [SerializeField] Transform house;
    Camera MainCamera;
    bool needToMove = false;
    public static int countOfBlock = 0;
    public static int hard = 0;
    float yPosForCamera = 0;

    public float distanseForCrane = 2f;
    float scaleForBottomDistance = 7f;

    public void InitCamera(Transform Crane, Transform house)
    {

        this.Crane = Crane ;
        this.house = house;
        MainCamera = Camera.main;
        CameraExtension();
        StartCoroutine(CameraScaler());
        //Crane.position = new Vector3(Crane.transform.position.x, transform.position.y + MainCamera.orthographicSize );
        countOfBlock = 0;
        hard = 0;


        if (MainCamera.orthographicSize > 13)
        {
            distanseForCrane = 1.5f;
            scaleForBottomDistance = 5f;
        }
        else if (MainCamera.orthographicSize <= 13)
        {
            distanseForCrane = 2f;
            scaleForBottomDistance = 6f;
        }

    }
    // Update is called once per frame
    void Update()
    {

        if (CraneController.instance.listOfBlocks.Count == 1)
        {
            Crane.position = new Vector3(Crane.transform.position.x, MainCamera.rect.y + transform.position.y + MainCamera.orthographicSize * distanseForCrane);
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, house.position.y + MainCamera.orthographicSize + 3.9f * countOfBlock, -10), 0.1f);


        }
        else
        {

            if (MainCamera.orthographicSize > 13)
                distanseForCrane = 1.5f;
            else if (MainCamera.orthographicSize <= 13)
                distanseForCrane = Mathf.Lerp(distanseForCrane,  1.8f,0.1f);
            Crane.position = new Vector3(Crane.transform.position.x, MainCamera.rect.y + transform.position.y + MainCamera.orthographicSize * distanseForCrane);



            float buff = yPosForCamera;
            yPosForCamera = CraneController.instance.listOfBlocks[CraneController.instance.listOfBlocks.Count - 2].transform.position.y + MainCamera.orthographicSize - MainCamera.orthographicSize / scaleForBottomDistance;

            if (buff > yPosForCamera)
                yPosForCamera = buff;

           transform.position = Vector3.Lerp(transform.position, new Vector3(0, yPosForCamera, -10), 0.1f);
        }
    }
    IEnumerator CameraScaler()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

            if (Mathf.Abs(min.x + 9) <= 0.1f)
                break;

            // Debug.Log(Mainamera.orthographicSize);

            if (min.x < -9)
            {
                MainCamera.orthographicSize -= 0.2f;
            }
            else if (min.x > -9)
            {
                MainCamera.orthographicSize += 0.2f;
            }
        }
    }


    void CameraExtension()
    {
        float x = (float)Screen.height / Screen.width;
        if (x >= 1.3f && x <= 1.4f)
        {
            // Debug.Log("x >= 1.3f && x <= 1.4f");
            MainCamera.orthographicSize = 12;
        }
        if (x >= 1.666 && x < 1.7)
        {
            // Debug.Log("x >= 1.666 && x < 1.7");
            MainCamera.orthographicSize = 15;
        }


        if (x >= 1.7 && x < 1.8)
        {
            // Debug.Log("x >= 1.7 && x < 1.8");
            MainCamera.orthographicSize = 16;
        }
        if (x >= 1.8 && x < 2)
        {
            //Debug.Log("x >= 1.7 && x < 2");
            MainCamera.orthographicSize = 17;
        }


        if (x == 2)
        {
            // Debug.Log("x == 2");
            MainCamera.orthographicSize = 18.4f;
        }

        if (x > 2)
        {
            // Debug.Log("x == 2");
            transform.localScale = new Vector3(23.7f, 23.7f, 23.7f);
        }
        // Debug.Log("x = " + x);
    }
}
