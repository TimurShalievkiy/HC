using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    //верхняя стена
    public GameObject uperWall;
    //нижняя стена если есть
    public GameObject downWall;


    //левая стена
    public GameObject leftWall;
    //правая стена
    public GameObject rightWall;

    //стартовая позиция ракетки
    Vector3 startPos;
    Vector3 dif;
    public float rpcketSpead = 6f;

    private void Start()
    {
        //получение скорость ракети
        rpcketSpead = JsonFileWriter.data.rocketSpeed;
    }

    void FixedUpdate()
    {
        //если есть тач
        if (Input.touchCount > 0)
        {
            //создаем рейкаст
            RaycastHit hit;

            //пускаем рей
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //если есть касание
            if (Physics.Raycast(ray, out hit))
            {
                //если тач начался
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    //получаем разницу между касанием и текущей позицией
                    dif = hit.point - transform.position;
                }

                //получаем новую позицию
                startPos = hit.point - dif;

                //выставляем базовую высоту
                startPos.y = 0.05f;

                //ограничение ракетки в длину
                startPos.z = Mathf.Clamp(startPos.z,  uperWall.transform.position.z + 0.25f, downWall.transform.position.z - 0.22f);

                //ограничение ракетки в ширь
                startPos.x = Mathf.Clamp(startPos.x, rightWall.transform.position.x + 1.15f, leftWall.transform.position.x - 1.12f);

                //перемещение ракетки в новую позицию
                transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * rpcketSpead);
            }
        }

    }


}

