using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchZonesCreator : MonoBehaviour
{
    Transform firstTouchZone;
    Transform secondTouchZone;
    Transform thirdTouchZone;




    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
  
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
        instance.transform.localPosition =new Vector2( x, 0);







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
        int x = Random.Range(1, 20);
        //Debug.Log(x);
        return Instantiate(Resources.Load("Shapes/"+x, typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
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
