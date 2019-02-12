using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneClaster : MonoBehaviour {
    
    //флаг есть ли в зоне за кластером шайба
    bool ball = false;
    //флаг есть ли в зоне за кластером ракетка
    bool rocket = false;


    //проверка наличия в тригерной зоне ракетки и шайбы
    private void OnTriggerEnter(Collider other)
    {
        //если вошел в тригер мяч
        if (other.transform.GetComponent<Ball>() != null)
        {
            //то меняем флаг
            ball = true;
        }
        //если в тригер вошла ракетка 
        if (other.transform.GetComponent<RocketController>() != null)
        {
            //то меняем флаг
            rocket = true;
        }

        //если и шайба и ракетка в тригерной зоне
        if (ball && rocket)
        {
            //выставляем количество кластеров в 0 в результате создастся новый кластер
            ClasterCreator.counter = 0;
             // уичтожаем остатки кластера
            Destroy(this.transform.parent.gameObject);
            
        }

    }
    private void OnTriggerExit(Collider other)
    {
        //если шайба вышла с тригера
        if (other.transform.GetComponent<Ball>() != null)
        {
            //возвращаем флаг
            ball = false;
        }
        //если ракетка вышла с тригера
        if (other.transform.GetComponent<RocketController>() != null)
        {
            //возвращаем флаг
            rocket = false;
        }
    }
}
