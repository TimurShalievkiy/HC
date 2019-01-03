using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claster : MonoBehaviour {

    //скорость движения кластера
    public float speed ;
	// Use this for initialization

	void Start () {
        //стартовое движение кластера умножено на 10 для быстрого подхода к игровой области. В класе Wall выставляется обычная скорость
        speed = JsonFileWriter.data.clasterSpeed * 10;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.forward* Time.deltaTime * speed;
	}
    
}
