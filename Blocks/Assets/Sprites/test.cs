using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    //клас созданый для прелоудера 
    //вращает буковки Playcus и останавливает их по таймеру


        //таймер до остановки
    public float timer = 1;


    // Update is called once per frame
    void Update()
    {
        //если таймер не ушел за 0
        if (timer >= 0)
        {
            //вращаем букву
            transform.Rotate(Vector3.left, +5000 * Time.deltaTime);
            //отнимаем прошедшее время
            timer -= Time.deltaTime;
        }
        else
            //когда таймер меньше 0 останавливаем букву с нулевым углом вращения
            transform.localRotation = new Quaternion(0, 0, 0, 0);
    }
}
