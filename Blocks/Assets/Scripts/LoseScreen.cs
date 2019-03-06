using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{

    //отображение текущего счета 
    public Text currentScore;

    // отображение текущего счета
    public Text bestScore;

    //количество звезд
    public Text stars;


    //метод для передачи значений в луз скрин
    public void SetLoseScreenValue()
    {
        //назначение отображения текущего счета
        currentScore.text = "Score: " + ScoreManager.currentScore.ToString();

        //Отображение текущего результата
        bestScore.text ="Best: " +  ScoreManager.GetPreviousBestScore().ToString();

        //отображение количества звезд
        stars.text = "Stars: " + 0;

        //если текущий счет не равен лучшему вызываем метод записи нового результата в скор менеджере
        //там проверка на то больше ли текущий результат предыдущего
        if(ScoreManager.currentScore != ScoreManager.GetPreviousBestScore())
        ScoreManager.SaveResult();

    }
}
