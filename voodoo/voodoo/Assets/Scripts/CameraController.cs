using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Цель
    public Transform target;

    //Скорость слежения
    public float speed = 10f;

    //Маска слоев препятствий
   // public LayerMask maskObstacles;

    private Vector3 _position;
    [SerializeField] Vector3 offset;
    void Start()
    {
        _position = target.InverseTransformPoint(transform.position);
    }

    void Update()
    {
        var oldRotation = target.rotation;
        target.rotation = Quaternion.Euler(0, oldRotation.eulerAngles.y, 0);

        var currentPosition = target.TransformPoint(_position);

        //currentPosition.y = transform.position.y;


        target.rotation = oldRotation;

        transform.position = Vector3.Lerp(transform.position, currentPosition, speed * Time.deltaTime);

        var currentRotation = Quaternion.LookRotation(target.position - transform.position);

        currentRotation.x = 0;
        currentRotation.z = 0;
        currentRotation.y = target.rotation.y -0.08f;

        transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, speed * Time.deltaTime);


    }
}
//public Transform target;

//public float speed = 4f;

//public LayerMask maskObstacles;

//Vector3 _position;

//private void Start()
//{
//    _position = target.InverseTransformPoint(transform.position);
//}

//private void Update()
//{
//    var oldRot = target.rotation;
//    target.rotation = Quaternion.Euler(0, oldRot.eulerAngles.y, 0);
//    var currentPosition = target.TransformPoint(_position);
//    target.rotation = oldRot;

//    currentPosition.y = transform.position.y;
//    //currentPosition.z = target.transform.position.z;
//    currentPosition.x = target.transform.position.x+18;

//    transform.position = Vector3.Lerp(transform.position, currentPosition, speed * Time.deltaTime);
//    var currentRotatin = Quaternion.LookRotation(target.position - transform.position);
//    currentRotatin.x = 0;
//    currentRotatin.y = target.rotation.y;
//    transform.rotation = Quaternion.Lerp(transform.rotation, currentRotatin, speed * Time.deltaTime);
//}