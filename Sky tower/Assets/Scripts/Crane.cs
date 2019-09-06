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




    [SerializeField] float speed = 0.55f;
    [SerializeField] float currentSpeed = 0;

    bool isMoveRight = true;
    bool isMoveup = true;
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;

        currentAngle = maxAngle;
        buffAngl = currentAngle;
        bufForPersrnt = currentAngle+ buffAngl;

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
                float t = GetSpeedRotation();
                transform.eulerAngles += new Vector3(0, 0, Time.deltaTime * t);
                currentAngle -= Time.deltaTime * t;
            }
            else
            {
                isMoveRight = false;
                float x = Random.Range(minAngle, maxAngle);
                currentAngle = x + buffAngl;
                bufForPersrnt = currentAngle;
                buffAngl = x;
            }
        }
        else
        {

            if (currentAngle >= 2)
            {
                float t = GetSpeedRotation();

                transform.eulerAngles += new Vector3(0, 0, Time.deltaTime * -t);
                currentAngle -= Time.deltaTime * t;
            }
            else
            {
                isMoveRight = true;
                // float x = Random.Range(minAngle, maxAngle);
                currentAngle = buffAngl + buffAngl;
                bufForPersrnt = currentAngle;

            }

        }
    }

    float GetSpeedRotation()
    {
        float t = 0;
        float quadOfWay = bufForPersrnt / 4;

        float percentOfQuad = (bufForPersrnt - currentAngle - (bufForPersrnt - quadOfWay)) / quadOfWay;


        if (currentAngle < bufForPersrnt / 4)
        {


            if (percentOfQuad >= 0.8f)
                percentOfQuad = 0.8f;
            if (percentOfQuad <= 0.2)
            {
                t = rotationSpeed;
            }
            else
            {
                t = rotationSpeed - (rotationSpeed * percentOfQuad);
                // Debug.Log("(" + (bufForPersrnt - currentAngle) + " - (" + (bufForPersrnt - quadOfWay) + "))/" + quadOfWay + " = " + percentOfQuad);

            }
        }
        else if (currentAngle > bufForPersrnt - quadOfWay)
        {
            percentOfQuad = (bufForPersrnt - currentAngle) / quadOfWay;
            if (percentOfQuad <= 0.3f)
                percentOfQuad = 0.3f;

            Debug.Log(percentOfQuad);
            t = rotationSpeed * percentOfQuad;
        }
        else
        {
           
            t = rotationSpeed;
        }
        //if (currentAngle <= bufForPersrnt / 4)
        //{
        //    Debug.Log((currentAngle/ bufForPersrnt*100)+"%");

        //    if (currentSpeed >= 0.3f)
        //        currentSpeed -= 1* Time.deltaTime;

        //    t = rotationSpeed * currentSpeed;

        //}
        ////else if (currentAngle >= bufForPersrnt - bufForPersrnt /4)
        ////{
        ////    if (currentSpeed >= 0.3f)
        ////        currentSpeed += Time.deltaTime;
        ////    else
        ////        currentSpeed = 0.3f;

        ////    t = rotationSpeed * currentSpeed;

        ////}
        //else
        //{
        //    currentSpeed = speed;
        //    t = rotationSpeed;

        //}





        // Debug.Log(t);
        //if (currentAngle - buffAngl <= 0)
        //{
        //    t = rotationSpeed * Mathf.Abs(currentAngle / bufForPersrnt) ;
        //    Debug.Log(Mathf.Abs(currentAngle / bufForPersrnt));
        //    currentSpeed = speed;
        //}

        //else
        //{
        //    if(currentSpeed>=0)
        //    currentSpeed -= Time.deltaTime; ;
        //    t = rotationSpeed - (rotationSpeed * Mathf.Abs(currentAngle) / (bufForPersrnt) * currentSpeed);

        //        t = rotationSpeed - (rotationSpeed * (Mathf.Abs(currentAngle) / (bufForPersrnt) * 0.1f));          
        //}
        return t;
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
