using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float distanceToBlock;
    [SerializeField] Transform Crane;
    [SerializeField] Transform home;
    Camera camera;
    bool needToMove = false;
    public static int countOfBlock = 0;
    
    private void Start()
    {
        camera = Camera.main;
        Crane.position = new Vector3(Crane.transform.position.x,  transform.position.y + camera.orthographicSize + 7);

    }
    // Update is called once per frame
    void Update()
    {
       // if (needToMove)
       // { 
            Crane.position =new Vector3(Crane.transform.position.x,  camera.rect.y + transform.position.y + camera.orthographicSize + 7);
            transform.position =Vector3.Lerp(transform.position, new Vector3(0, home.position.y+ camera.orthographicSize + 3.9f* countOfBlock, -10),0.1f);
            //transform.position += new Vector3(0, 2 * Time.deltaTime);
       // }
    }
    
}
