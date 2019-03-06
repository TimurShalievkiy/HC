using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaylyBonusManager : MonoBehaviour
{

    //обькты обозначающие текущий дейлик
    public GameObject firstDay;
    public GameObject secondDay;
    public GameObject thirdDay;
    public GameObject fourDay;
    public GameObject fiveDay;
    public GameObject sixDay;
    public GameObject sevenDay;

    //в старте делаем проверку на текущий дейлик
    private void Start()
    {
        ChechDaylyBonus();                      
    }

    //сбор награды дейлика по клику на кнопку
    public void CollectButtonClick()
    {
        //Debug.Log("revard = " + GetRevardForBonus(GetCurrentDayDailyBonus()));
        IncrementDay();
    }

    //визуальное отображение какие дейлики задействованы 
    public  void ChechDaylyBonus()
    {
        //получаем текущий номер дня 
        int x = GetCurrentDayDailyBonus();

        //подсвечиваем зеленым уже открытые
        for (int i = 1; i < x+1; i++)
        {
            switch (i)
            {
                case 1:
                    firstDay.GetComponent<Image>().color = Color.green;
                    break;
                case 2:
                    secondDay.GetComponent<Image>().color = Color.green;
                    break;
                case 3:
                    thirdDay.GetComponent<Image>().color = Color.green;
                    break;
                case 4:
                    fourDay.GetComponent<Image>().color = Color.green;
                    break;
                case 5:
                    fiveDay.GetComponent<Image>().color = Color.green;
                    break;
                case 6:
                    sixDay.GetComponent<Image>().color = Color.green;
                    break;
                case 7:
                    sevenDay.GetComponent<Image>().color = Color.green;

                    break;
            }
        }
       
    }
    //получить текущий день дейли бонуса
    public  int GetCurrentDayDailyBonus()
    {
        int x = 1;
        if (PlayerPrefs.HasKey("dayNum"))
        {
            x = PlayerPrefs.GetInt("dayNum");

            if (x > 7)
            {
                PlayerPrefs.SetInt("dayNum", 1);
                x = 1;
            }
        }
        else
        {
            PlayerPrefs.SetInt("dayNum", 1);        
        }
        return x;
    }
    //инкрементируем значение дня дейли бонуса
    void IncrementDay()
    {
        if (PlayerPrefs.HasKey("dayNum"))
        {
            PlayerPrefs.SetInt("dayNum", PlayerPrefs.GetInt("dayNum")+1);
        }
        else
            PlayerPrefs.SetInt("dayNum", 1);
    }

    //получить значение награды по номеру текущего дня
    public  int GetRevardForBonus(int numDay)
    {

        switch (numDay)
        {
            case 1:
                return 10;
                break;
            case 2:
                return 15;
                break;
            case 3:
                return 20;
                break;
            case 4:
                return 35;
                break;
            case 5:
                return 45;
                break;
            case 6:
                return 60;
                break;
            case 7:
                return 80;
                break;
        }

        return 0;
    }

}
