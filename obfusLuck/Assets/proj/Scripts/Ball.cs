using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {


    public float speedOfBall = 1;


	void Start () {
        speedOfBall = JsonFileWriter.jsondata.speedOfBall;

        this.transform.GetComponent<Rigidbody>().velocity = Vector3.back * speedOfBall;
        
    }




    float HitOfFactor(Vector3 PosOfBall, Vector3 PosOfRoscker, float heightOfRocket)
    {      
        return ( PosOfBall.x - PosOfRoscker.x) / heightOfRocket;
    }
    void Update()
    {
        if (this.transform.GetComponent<Rigidbody>().velocity.magnitude != speedOfBall)
        {
            this.transform.GetComponent<Rigidbody>().velocity = this.transform.GetComponent<Rigidbody>().velocity.normalized * speedOfBall;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<RocketController>() != null)
        {
            float vx = HitOfFactor(this.transform.position, collision.transform.position, collision.collider.bounds.size.x);
            Vector3 dir = new Vector3(vx, 0f, -1f);
            this.transform.GetComponent<Rigidbody>().velocity = dir * speedOfBall;
        }
    }
}
