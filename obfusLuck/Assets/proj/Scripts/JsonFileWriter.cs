using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonFileWriter : MonoBehaviour
{
    //сериализуемый обьект 
    public static JsonData jsondata;



    public void Start()
    {
        TextAsset file = Resources.Load("data") as TextAsset;
        string contentOFdata = file.ToString();
        

        jsondata = JsonUtility.FromJson<JsonData>(contentOFdata);
    }


}

[Serializable]
public class JsonData
{
    public int speedOfRocket;
    public int speedOfBall ;
    public float speedOfClaster;

    public JsonData(int speedOfRocket, int speedOfBall, float speedOfClaster)
    {
        this.speedOfRocket = speedOfRocket;
        this.speedOfBall = speedOfBall;
        this.speedOfClaster = speedOfClaster;
    }

    public JsonData() { }
}

