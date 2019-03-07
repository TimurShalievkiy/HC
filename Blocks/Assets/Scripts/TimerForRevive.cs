using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerForRevive : MonoBehaviour
{

    //обьект луз панели
    public GameObject loseScreen;

    //рисунок круга который уменьшается по времени
    public Image progresImage;

    //текст таймера
    public Text timerText;

    //продолжительность таймера
    public float duration = 5;

    //текущее значение таймера
    float currentDuration;

    //процент для правильного отображения  круга
     float percent = 0;



    // Start is called before the first frame update
    void Start()
    {
        //сбрасываем таймер
        ResetTimer();
    }

    //метод сброса таймера
    public void ResetTimer()
    {
        //текущее значение таймера ставим в изначальное
        currentDuration = duration;

        //заполненость круга ставим в 1 тоесть заполнен
        progresImage.fillAmount = 1;

        //значение текста таймера выставляем согласно текущему значению
        timerText.text = duration.ToString();

        //процент равен базовое значение разделено на сто
        percent = duration / 100;
    }
    void Update()
    {
        //если текущее значение таймера больше равно нулю то выполняем действия с таймером по отображению и уменьшению
        if (currentDuration >= 0)
        {
            //уменьшаем текущее значение согласно прошедшему времени
            currentDuration -= Time.deltaTime;

            //если текущее значение больше 0
            if (currentDuration > 0)
            {
                //для лучшего обозначения прибавляем к текущему значению таймера 1 и округляем до ближайшего целого
                //отображаем текущее значение в тексте таймера
                timerText.text = ((int)currentDuration + 1).ToString();
            }
            else
            {
                //если меньше 0 то отобразим текущее значени...это вообще вызывается?
                timerText.text = ((int)currentDuration).ToString();
            }

            //выставляем значение отображения круга в зависимотси от текущего значения таймера
            progresImage.fillAmount = (currentDuration / percent) / 100;
        }
        else {
            //если таймер меньше 0 то отображаем окно проиграша
            loseScreen.GetComponent<LoseScreen>().SetLoseScreenValue();
            //активируем панель проиграша
            loseScreen.SetActive(true);
            //убираем текущую панель
            gameObject.SetActive(false);
        }
    }


}
