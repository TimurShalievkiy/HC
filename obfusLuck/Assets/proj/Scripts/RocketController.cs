using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public GameObject upWall;
    public GameObject down_Wall;
    public GameObject left_Wall;
    public GameObject right_Wall;

    Vector3 startPosOfRocket;
    Vector3 erence;
    public float rpcketSpead = 6f;



    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    erence = hit.point - transform.position;
                }
                startPosOfRocket = hit.point - erence;

                startPosOfRocket.y = 0.05f;

                startPosOfRocket.z = Mathf.Clamp(startPosOfRocket.z,  upWall.transform.position.z + 0.25f, down_Wall.transform.position.z - 0.22f);

                startPosOfRocket.x = Mathf.Clamp(startPosOfRocket.x, right_Wall.transform.position.x + 1.15f, left_Wall.transform.position.x - 1.12f);

                transform.position = Vector3.MoveTowards(transform.position, startPosOfRocket, Time.deltaTime * rpcketSpead);
            }
        }

    }

    private void Start()
    {
        rpcketSpead = JsonFileWriter.jsondata.speedOfRocket;
    }
}

