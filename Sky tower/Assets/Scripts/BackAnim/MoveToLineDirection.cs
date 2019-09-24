using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLineDirection : MonoBehaviour
{
    [SerializeField] float speed = 2;

    public void ChangeSpeed(float x)
    {
        speed = x;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, 0);
    }
}
