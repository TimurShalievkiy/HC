using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    //клас созданый для прелоудера 
    //вращает буковки Playcus и останавливает их по таймеру


    //время таймера  до остановки
    public float timer = 0.3f;

    //скорость вращения букв
    public int rotationSpeed = 5000;

    //текущее значение таймера
    float currentTimer;


    //номер следующей буквы которая будет остановлена 
     int counter = 0;


    //выставляем базовое значение таймера
    private void Start()
    {
        currentTimer = timer;
    }
    // Update is called once per frame
    void Update()
    {
        //если таймер не ушел за 0
        if (currentTimer >= 0)
        {
            //вращаем буквы в панели со словами
            for (int i = counter; i < transform.childCount; i++)
            {
               transform.GetChild(i).transform.Rotate(Vector3.left, +rotationSpeed * Time.deltaTime);
            }

            //отнимаем прошедшее время
            currentTimer -= Time.deltaTime;
        }
        //когда таймер меньше 0 останавливаем букву с нулевым углом вращения
        else
        {
            //если количество дочерних елементов больше номера следующего номера буквы для остановки
            if (transform.childCount > counter)
            {
                //останавливаем букву с заданным номером для остановки
                transform.GetChild(counter).transform.localRotation = new Quaternion(0, 0, 0, 0);

                //обновляем таймер
                currentTimer = timer;

                //увеличиваем номер следующей буквы для остановки
                counter++;
            }
        } 
    }
}
