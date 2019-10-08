using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByVector : MonoBehaviour
{
    [SerializeField] float minSpeed = 1;
    [SerializeField] float maxSpeed = 3;
    [SerializeField] float currentSpeed = 0;
    Vector2 direction;

    public void ChangeSpeed(float x, float y,float min,float max)
    {
        direction.x = x;
        direction.y = y;
        currentSpeed = Random.Range(min, max);
    }
    // Update is called once per frame

    private void Update()
    {
        transform.position += new Vector3(direction.x * currentSpeed * Time.deltaTime, direction.y * currentSpeed * Time.deltaTime, 0);

    }
   
}
