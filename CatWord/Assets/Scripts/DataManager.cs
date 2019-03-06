﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class JsonData
{
    // Foreign language word in first form
    public string first;

    // Word in second form
    public string second;

    // Word in third form
    public string third;

    // Word translation
    public string translation;

    public JsonData(string first, string second, string third, string translation)
    {
        this.first = first;
        this.second = second;
        this.third = third;
        this.translation = translation;
    }

}

[Serializable]
public class MyWrapper
{
    public List<JsonData> words;
}

public class DataManager : MonoBehaviour
{
    // List of JsonData objects
    public static MyWrapper data;
    public static int index = 0;

    public void Start()
    {
        // Get data from JSON file
        TextAsset file = Resources.Load("Irregular verbs/irregular") as TextAsset;
        string content = file.ToString();

        data = JsonUtility.FromJson<MyWrapper>(content);

      //  data.words.ForEach(delegate (JsonData word) {
       //     Debug.Log(word.first + " - " + word.second + " - " + word.third
       //         + " : " + word.translation);
       // });
    }
    public JsonData GetNextWord()
    {
        if(index >= data.words.Count)
            return data.words[data.words.Count-1];
        return data.words[index];
    }
    public JsonData GetByWord(string str)
    {
        foreach (var item in data.words)
        {
            if (item.first == str)
                return item;
        }
        return null;
    }
    public void IncrementIndex()
    {
       // if()
        index++;
    }
}