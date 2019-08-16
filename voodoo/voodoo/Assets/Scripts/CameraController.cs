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

    [SerializeField] float angle= 0.01f;
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
        currentRotation.y = 0;
        currentRotation.y = target.rotation.y - angle;

        transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, speed * Time.deltaTime);


    }
}
