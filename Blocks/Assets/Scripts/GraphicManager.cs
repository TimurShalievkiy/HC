using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicManager : MonoBehaviour
{
    
    //ссылка на бекграунд игрового окна
    public GameObject background;
    // Start is called before the first frame update
    void Start()
    {
        //выполняем замену спрайтов на соответсвующие текущему скину
        SetGraphicValues();
    }

    void SetGraphicValues()
    {
        //задать спрайт бекграунда по текущему скину
        background.GetComponent<Image>().sprite = ScinManager.GetBackground();
    }
}
