using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float distanceToBlock;
    [SerializeField] Transform Crane;
    [SerializeField] Transform home;
    Camera Mainamera;
    bool needToMove = false;
    public static int countOfBlock = 0;
    public static int hard = 0;

    private void Start()
    {
        Mainamera = Camera.main;
        Crane.position = new Vector3(Crane.transform.position.x,  transform.position.y + Mainamera.orthographicSize + 7);
        countOfBlock = 0;
        hard = 0;
    }
    // Update is called once per frame
    void Update()
    {

            Crane.position =new Vector3(Crane.transform.position.x,  Mainamera.rect.y + transform.position.y + Mainamera.orthographicSize + 7);
            transform.position =Vector3.Lerp(transform.position, new Vector3(0, home.position.y+ Mainamera.orthographicSize + 3.9f* countOfBlock, -10),0.1f);

    }
    
}
