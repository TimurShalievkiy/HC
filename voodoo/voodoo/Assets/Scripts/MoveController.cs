using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    float force = 800;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Transform[] points;
    [SerializeField] float speed = 2;
    float currentSpeed = 2;

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
            float x = 0;
            float z = 0;
            if (transform.position.x == points[0].position.x)
            {

                if (transform.position.z > points[0].position.z)
                    z = force;
                else
                    z = -force;
            }

            if (transform.position.z == points[0].position.z)
            {

                if (transform.position.x > points[0].position.x)
                    x = force;
                else
                    x = -force;
            }
            Debug.Log(collision.transform.name);
            doMove = false;
            rigidbody.AddForce(new Vector3(0, force, z),ForceMode.Impulse);
            currentSpeed = 0;
            StartCoroutine(WaitAndGo());
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
        if (currentSpeed <= speed)
        {
            currentSpeed += 0.2f;
        }

        Vector3 a = Vector3.MoveTowards(transform.position, points[0].position, 10 * currentSpeed * Time.deltaTime);
        a.y = transform.position.y;
        transform.position = a;
    }
    IEnumerator WaitAndGo()
    {
        yield return new WaitForSeconds(1);
        doMove = true;
        StopCoroutine(WaitAndGo());
    }

    

}
