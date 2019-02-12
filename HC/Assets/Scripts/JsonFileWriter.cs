using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonFileWriter : MonoBehaviour
{
    //сериализуемый обьект 
    public static JsonData data;



    public void Start()
    {
        //базовая инициализация
        //data = new JsonData(12, 10,0.5f);

        //получение данных с json
        TextAsset file = Resources.Load("data") as TextAsset;
        string content = file.ToString();
        

        data = JsonUtility.FromJson<JsonData>(content);
    }


}

[Serializable]
public class JsonData
{
    //скорость ракетки
    public int rocketSpeed;

    //скорость шайбы
    public int ballSpeed ;

    //скорсть кластера
    public float clasterSpeed;

    public JsonData(int rocketSpeed, int ballSpeed, float clasterSpeed)
    {
        this.rocketSpeed = rocketSpeed;
        this.ballSpeed = ballSpeed;
        this.clasterSpeed = clasterSpeed;
    }

    public JsonData() { }
}

