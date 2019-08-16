using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveController : MonoBehaviour
{
    [SerializeField] float force = 10000;
    [SerializeField] Rigidbody thisRigidbody;
    [SerializeField] Transform[] points;
    [SerializeField] float speed = 2;


    [SerializeField] GameObject brick;
    public static float currentSpeed = 2;

    int indexWayPoint = 0;

    public static bool isPush = false;
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
                Handheld.Vibrate();
                isPush = true;
                Jelly.endOfShadow.gameObject.SetActive(false);


                doMove = false;
                Vector3 forseVector = GetForceDirection();

                transform.position += forseVector / 1500;
                thisRigidbody.AddForce(GetForceDirection(), ForceMode.Impulse);
                currentSpeed = 0;
                StartCoroutine(WaitAndGo());
                
            }

            GameObject g = Instantiate(brick, collision.transform.position, Quaternion.identity);
            GameObject g2 = Instantiate(brick, collision.transform.position, Quaternion.identity);
            g.SetActive(true);
            g2.SetActive(true);

            float impuls = 25;

            g.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-impuls, impuls), Random.Range(impuls, impuls), Random.Range(-impuls, impuls)),ForceMode.Impulse);
            g2.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-impuls, impuls), Random.Range(impuls, impuls), Random.Range(-impuls, impuls)), ForceMode.Impulse);

            Destroy(g, Random.Range(5, 10));
            Destroy(g2, Random.Range(5, 10));
            collision.transform.gameObject.SetActive(false);
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

 
        if (indexWayPoint < points.Length &&Vector3.Distance(transform.position, points[indexWayPoint].position) < 5f)
        {
            //Debug.Log("point count = " +  points.Length  + " index = " + indexWayPoint);
            if (indexWayPoint < points.Length )
            {
                indexWayPoint++;
          ;
            }
            else
            {
                doMove = false;
        
            }

        }


            if (currentSpeed <= speed)
            {
                currentSpeed += 0.2f;
            }
        if (indexWayPoint < points.Length)
        {
            Vector3 a = Vector3.MoveTowards(transform.position, points[indexWayPoint].position, 10 * currentSpeed * Time.deltaTime);
            a.y = transform.position.y;

            Vector3 dir = GetForceDirection();

            if (dir.x != 0)
            {
                a.z = points[indexWayPoint].position.z;

            }
                

            if (dir.z != 0)
            {
                a.x = points[indexWayPoint].position.x;
        
            }

            transform.position = a;


            TransformExtensions.LookAtXZ(transform, points[indexWayPoint].position);
        }
        else {
            
            if (SceneManager.GetActiveScene().name == "1")
                SceneManager.LoadScene("2");
            else if (SceneManager.GetActiveScene().name == "2")
                SceneManager.LoadScene("3");
        }
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
            if (points[indexWayPoint - 1].position.x == points[indexWayPoint].position.x)
            {
                if (points[indexWayPoint - 1].position.x > points[indexWayPoint].position.x)
                {
                    z = force;
                }
                else
                {
                    z = -force;

                }
            }
            if (points[indexWayPoint - 1].position.z == points[indexWayPoint].position.z)
            {
                if (points[indexWayPoint - 1].position.z > points[indexWayPoint].position.z)
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

    }

    

}
public static class TransformExtensions
{
    public static void LookAtXZ(this Transform transform, Vector3 point)
    {
        var direction = (point - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public static void LookAtXZ(this Transform transform, Vector3 point, float speed)
    {
        var direction = (point - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), speed);
    }
}