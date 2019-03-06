﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchZonesCreator : MonoBehaviour
{
    Transform firstTouchZone;
    Transform secondTouchZone;
    Transform thirdTouchZone;

    List<GameObject> lo;


    public Slider slider;
    // Start is called before the first frame update
    void MixList()
    {
        lo = new List<GameObject>();

        foreach (var item in Resources.LoadAll("Prefs/"))
        {
            lo.Add(item as GameObject);
        }
        int x = 0;

        for (int i = lo.Count - 1; i >= 1; i--)
        {
            x = Random.Range(0, lo.Count);
            //Debug.Log(x);
            var temp = lo[x];
            lo[x] = lo[i];
            lo[i] = temp;
        }
    }
    void Start()
    {
        MixList();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            GenerateNewWaveOfShape();
        }
    }

    void GenerateNewWaveOfShape()
    {


        float x  = 0;
        GameObject instance = GetNextShape();
        instance.transform.parent = transform;
        instance.transform.localScale = new Vector3(1, 1, 1);


        x = instance.transform.position.x + instance.transform.GetComponent<RectTransform>().sizeDelta.x;
        Debug.Log("1 = " + instance.transform.position.x + " " + instance.transform.GetComponent<RectTransform>().sizeDelta.x);
        instance.transform.localPosition =new Vector2(instance.transform.localPosition.x + instance.transform.GetComponent<RectTransform>().sizeDelta.x, 0);




        GameObject instance2 = GetNextShape();
        instance2.transform.parent = transform;
        instance2.transform.localScale = new Vector3(1, 1, 1);



        GameObject instance3 = GetNextShape();
        instance3.transform.parent = transform;
        instance3.transform.localScale = new Vector3(1, 1, 1);

        x = instance3.transform.position.x + instance3.transform.GetComponent<RectTransform>().sizeDelta.x;
        instance3.transform.localPosition = new Vector2(-x, 0);
    }

    GameObject GetNextShape()
    {
        int x =  Random.Range(0, lo.Count);
        if (transform.childCount == 2)
            MixList();
        //int x = Random.Range(0, lo.Count);

        // return Instantiate(Resources.Load("Shapes/"+x, typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
        return Instantiate(lo[x], transform.position, Quaternion.identity) as GameObject;
    }

    public static void DestroyAllZones(Transform touchZonesParent)
    {

        for (int i = touchZonesParent.childCount-1; i >= 0; i--)
        {
            GameObject.Destroy(touchZonesParent.GetChild(i).gameObject);
        }
    }
    public  void DestroyAllZones()
    {

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }
}