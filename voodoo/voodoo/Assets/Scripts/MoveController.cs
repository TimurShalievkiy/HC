using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] float force = 10000;
    [SerializeField] Rigidbody thisRigidbody;
    [SerializeField] Transform[] points;
    [SerializeField] float speed = 2;
    float currentSpeed = 2;

    int indexWayPoint = 0;

    bool isPush = false;
    bool doMove = false;
    bool isStart = false;

    private void Start()
    {
        currentSpeed = speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "block")
        {
            if (!isPush)
            {
                isPush = true;

 

                doMove = false;
                Vector3 forseVector = GetForceDirection();

                transform.position += forseVector / 1500;
                thisRigidbody.AddForce(GetForceDirection(), ForceMode.Impulse);
                currentSpeed = 0;
                StartCoroutine(WaitAndGo());
                
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&!isStart)
        {
            doMove = true;
            isStart = true;

        }

        if (doMove)
         MoveToPoint();
    }

    void MoveToPoint()
    {
        if (Vector3.Distance(transform.position, points[indexWayPoint].position) <= 0.01f )
        {
            if (indexWayPoint <= points.Length - 1)
            {
                indexWayPoint++;
            }           
        }
 
        if (currentSpeed <= speed)
        {
            currentSpeed += 0.2f;
        }

        Vector3 a = Vector3.MoveTowards(transform.position, points[indexWayPoint].position, 10 * currentSpeed * Time.deltaTime);
        a.y = transform.position.y;

        Vector3 dir = GetForceDirection();
        if (dir.x != 0)
            a.z = points[indexWayPoint].position.z;

        if (dir.z != 0)
            a.x = points[indexWayPoint].position.x;

        transform.position = a;
    }

    Vector3 GetForceDirection()
    {

        float x = 0;
        float z = 0;
        if (indexWayPoint == 0)
        {
            if (points[0].position.x == points[1].position.x)
            {
                if (points[0].position.x > points[1].position.x)
                {

                    z = force;
                }
                else
                {

                    z = -force;

                }
            }
            if (points[0].position.z == points[1].position.z)
            {
                if (points[0].position.z > points[1].position.z)
                {

                    x = force;
                }

                else
                {

                    x = -force;
                }
            }
        }
        else
        {
            if (points[indexWayPoint-1].position.x == points[indexWayPoint].position.x)
            {
                if (points[indexWayPoint-1].position.x > points[indexWayPoint].position.x)
                {
                    z = force;
                }
                else
                {
                    z = -force;

                }
            }
            if (points[indexWayPoint-1].position.z == points[indexWayPoint].position.z)
            {
                if (points[indexWayPoint-1].position.z > points[indexWayPoint].position.z)
                {
                    x = force;
                }

                else
                {
                    x = -force;
                }
            }
        }

        return new Vector3(x,force/2,z);
    }

    IEnumerator WaitAndGo()
    {

        yield return new WaitForSeconds(1);
        doMove = true;
        StopCoroutine(WaitAndGo());
        isPush = false;
        //thisRigidbody.velocity = Vector3.zero;
    }

    

}
