using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour
{

    [SerializeField] float maxDistanseTop = 1;
    [SerializeField] float maxDistanseDown = 1;
    [SerializeField] float upDownSpeed = 1f;
    float currentHeight = 0;
    float buffHeight = 0;



    [SerializeField] float maxAngle = 20;
    [SerializeField] float minAngle = 1;
    [SerializeField] float currentAngle = 0;
    [SerializeField] float bufForPersrnt = 0;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float buffAngl = 0;


    bool isMoveRight = true;
    bool isMoveup = true;
    // Start is called before the first frame update
    void Start()
    {
        currentAngle = maxAngle;
        buffAngl = currentAngle;
        bufForPersrnt = currentAngle;

        currentHeight = (maxDistanseTop + maxDistanseDown) / 2;
        buffHeight = currentHeight;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCrane();
    }


    void MoveCrane()
    {
        RotateCrane();
        //MoveUpDownCrane();
    }
    private void RotateCrane()
    {
        if (isMoveRight)
        {
            //Debug.Log(transform.eulerAngles);

            if (currentAngle >= 2)
            {
               float t =rotationSpeed * Mathf.Abs(currentAngle / bufForPersrnt);
         
               transform.eulerAngles += new Vector3(0, 0, Time.deltaTime * t);
                currentAngle -= Time.deltaTime *t;
            }
            else
            {
                isMoveRight = false;
                float x = Random.Range(minAngle, maxAngle);
                currentAngle = x+ buffAngl ;
                bufForPersrnt = currentAngle;
                buffAngl = x;
            }
        }
        else
        {

            if (currentAngle >= 2)
            {
                float t = rotationSpeed * Mathf.Abs(currentAngle / bufForPersrnt);
                transform.eulerAngles += new Vector3(0, 0, Time.deltaTime * -t);
                currentAngle -= Time.deltaTime * t;
            }
            else
            {
                isMoveRight = true;
                float x = Random.Range(minAngle, maxAngle);
                currentAngle = x + buffAngl;
                bufForPersrnt = currentAngle;
                buffAngl = x;
            }
               
        }
    }

    private void MoveUpDownCrane()
    {
        if (isMoveup)
        {
            //Debug.Log(transform.eulerAngles);

            if (currentHeight >= 0)
            {
                transform.position += new Vector3(0, Time.deltaTime * upDownSpeed , 0);
                currentHeight -= Time.deltaTime * upDownSpeed;
            }
            else
            {
                isMoveup = false;
                float x = Random.Range(maxDistanseDown, maxDistanseTop);
                currentHeight = x + buffHeight;
                buffHeight = x;
            }
        }
        else
        {

            if (currentHeight >= 0)
            {
                transform.position += new Vector3(0, Time.deltaTime * -upDownSpeed, 0);
                currentHeight -= Time.deltaTime * upDownSpeed;
            }
            else
            {
                isMoveup = true;
                float x = Random.Range(maxDistanseDown, maxDistanseTop);
                currentHeight = x + buffHeight;
                buffHeight = x;
            }

        }
    }
}
