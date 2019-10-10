using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMuving : MonoBehaviour
{
    [SerializeField] string path ;
    [SerializeField] bool isright = true;
    [SerializeField] Vector2 direction ;
    [SerializeField] float minSpeed = 1;
    [SerializeField] float maxSpeed = 1;
    [SerializeField] float chance = 1;

    [SerializeField] bool withDelay = false;
    [SerializeField] float minDelay = 1;
    [SerializeField] float maxDelay = 1;
    [SerializeField] float deadTime = 5;

     enum Direction  { up, upLeft, upRight, midleLeft, midleRight, bottomLeft, bottomCentr, bottomRight, allBottom};
    [SerializeField] Direction spawnPos;


    public float maxY;
    public float miny;


    float delay = 0f;
    float currentDelay = 0f;
    void Update()
    {
        if (GameController.instance.mainCamera.transform.position.y + 10 > miny)
        {
            if (withDelay)
            {
                currentDelay += Time.deltaTime;
                if (currentDelay >= delay)
                {
                    CreateObject();
                    delay = Random.Range(minDelay,maxDelay);
                    currentDelay = 0;
                }
            }
            else 
            {
                if (Random.Range(0f, 100f) <= chance)
                {
                    CreateObject();
                }
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
                v = new Vector3(GameController.instance.mainCamera.transform.position.x - Camera.main.orthographicSize , Random.Range(miny, maxY));

                break;
            case Direction.midleRight:
               v = new Vector3(GameController.instance.mainCamera.transform.position.x + Camera.main.orthographicSize , Random.Range(miny, maxY));

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
            case Direction.allBottom:
                float x = Random.Range(-10,10);
                v = new Vector3(x, Random.Range(GameController.instance.mainCamera.transform.position.y - Camera.main.orthographicSize, Camera.main.transform.position.y - Camera.main.orthographicSize - 2));

                break;
            default:
                v = Vector3.zero;
                break;
        }

        return v;
    }
    void CreateObject()
    {
        GameObject[] list = Resources.LoadAll<GameObject>("BackObjects\\" + path);
        GameObject g = Instantiate(list[Random.Range(0, list.Length)]);


        if (g != null)
        {

            g.transform.position = GetSpawnPos();
            g.GetComponent<MoveByVector>().ChangeSpeed(direction.x, direction.y, minSpeed, maxSpeed,deadTime);
            g.transform.parent = transform;
            if (!isright)
                g.transform.localScale = new Vector3(-1, 1, 1);

            Destroy(g, deadTime);

            if (Camera.main.transform.position.y > maxY + 10)
                Destroy(this.gameObject);
        }
    }
}
