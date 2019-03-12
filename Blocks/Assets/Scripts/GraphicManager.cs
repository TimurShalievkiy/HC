using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicManager : MonoBehaviour
{
    public GameObject background;
    // Start is called before the first frame update
    void Start()
    {
        SetGraphicValues();
    }

    void SetGraphicValues()
    {

       // background.GetComponent<Image>().sprite = i;
        background.GetComponent<Image>().sprite = ScinManager.GetBackground();
       // Debug.Log(background.GetComponent<Image>().sprite);
    }
}
