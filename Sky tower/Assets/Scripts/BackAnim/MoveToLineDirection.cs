using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLineDirection : MonoBehaviour
{
    [SerializeField] float minSpeed = 1;
    [SerializeField] float maxSpeed = 3;
    [SerializeField] float currentSpeed = 0;

    private void Start()
    {
        ChangeSpeed(minSpeed, maxSpeed);
    }
    public void ChangeSpeed(float min, float max)
    {
        minSpeed = min;
        maxSpeed = max;
        currentSpeed = Random.Range(min, max);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-currentSpeed * Time.deltaTime, 0);
    }
}
