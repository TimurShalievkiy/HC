using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMuving : MonoBehaviour
{
    [SerializeField] string path ;
    [SerializeField] Vector2 direction ;
    [SerializeField] float minSpeed = 1;
    [SerializeField] float maxSpeed = 1;
    [SerializeField] float chance = 1;

    [SerializeField] float deadTime = 5;

     enum Direction  { up, upLeft, upRight, midleLeft, midleRight, bottomLeft, bottomCentr, bottomRight};
    [SerializeField] Direction spawnPos;


    public float maxY;
    public float miny;
    // Update is called once per frame
    void Update()
    {
      // if(GameController.instance.mainCamera.transform.position.y+10<miny)
        if (Random.Range(0, 100) <= chance)
        {
            GameObject[] list = Resources.LoadAll<GameObject>("BackObjects\\" + path);
            GameObject g = Instantiate(list[Random.Range(0,list.Length)]);

            if (g != null)
            {

                g.transform.position = GetSpawnPos();
                g.GetComponent<MoveByVector>().ChangeSpeed(direction.x, direction.y,minSpeed, maxSpeed);
                g.transform.parent = transform;

         
                Destroy(g, deadTime);

                if (Camera.main.transform.position.y > maxY + 10)
                    Destroy(this.gameObject);
            }
        }
    }

    Vector3 GetSpawnPos()
    {
        Vector3 v = Vector3.zero;

        switch (spawnPos)
        {
            case Direction.up:
                v = new Vector3(GameController.instance.mainCamera.transform.position.x, Random.Range(GameController.instance.mainCamera.transform.position.y + Camera.main.orthographicSize , Camera.main.transform.position.y+Camera.main.orthographicSize+2));
                break;
            case Direction.upLeft:
                v = new Vector3(GameController.instance.mainCamera.transform.position.x- Camera.main.orthographicSize- 2, Random.Range(GameController.instance.mainCamera.transform.position.y + Camera.main.orthographicSize, Camera.main.transform.position.y + Camera.main.orthographicSize + 2));

                break;
            case Direction.upRight:
                v = new Vector3(GameController.instance.mainCamera.transform.position.x + Camera.main.orthographicSize + 2, Random.Range(GameController.instance.mainCamera.transform.position.y + Camera.main.orthographicSize, Camera.main.transform.position.y + Camera.main.orthographicSize + 2));

                break;
            case Direction.midleLeft:
                v = new Vector3(GameController.instance.mainCamera.transform.position.x - Camera.main.orthographicSize , Random.Range(transform.position.y, maxY));

                break;
            case Direction.midleRight:
               v = new Vector3(GameController.instance.mainCamera.transform.position.x + Camera.main.orthographicSize , Random.Range(transform.position.y, maxY));

                break;
            case Direction.bottomLeft:
                v = new Vector3(GameController.instance.mainCamera.transform.position.x - Camera.main.orthographicSize , Random.Range(GameController.instance.mainCamera.transform.position.y - Camera.main.orthographicSize, Camera.main.transform.position.y - Camera.main.orthographicSize - 2));

                break;
            case Direction.bottomCentr:
                v = new Vector3(GameController.instance.mainCamera.transform.position.x, Random.Range(GameController.instance.mainCamera.transform.position.y - Camera.main.orthographicSize, Camera.main.transform.position.y - Camera.main.orthographicSize - 2));

                break;
             
            case Direction.bottomRight:
                v = new Vector3(GameController.instance.mainCamera.transform.position.x + Camera.main.orthographicSize + 2, Random.Range(GameController.instance.mainCamera.transform.position.y - Camera.main.orthographicSize, Camera.main.transform.position.y - Camera.main.orthographicSize - 2));

                break;
            default:
                v = Vector3.zero;
                break;
        }

        return v;
    }
}
