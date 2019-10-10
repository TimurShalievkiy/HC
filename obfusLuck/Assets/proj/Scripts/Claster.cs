using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claster : MonoBehaviour {

    public float speedOfClaster ;

	void Update () {
        transform.position += Vector3.forward* Time.deltaTime * speedOfClaster;
	}
    void Start()
    {
        speedOfClaster = JsonFileWriter.jsondata.speedOfClaster * 10;
    }

}
