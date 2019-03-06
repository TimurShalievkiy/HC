using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    //значение текущего счета игрока
    public static int currentScore = 0;

    //текст в котором отображается лучший рекорд игрока
    public Text bestScoreText;

    //текст обозначающий текущий счет игрока
    public Text currentScoreText;

    // Start is called before the first frame update
    void Start()
    {
        //сбрасываем счет
        ResetScore();
    }

    //получаем текущий лучший результат игрока сохраненного в 
    public static int GetPreviousBestScore()
    {
        int score = 0;
        if (PlayerPrefs.HasKey("BestScore"))
        {
            score = PlayerPrefs.GetInt("BestScore");
        }
        return score;
    }

    //сохранить лучший рекорд на устройстве
    public static void SaveResult()
    {
        if (GetPreviousBestScore() < currentScore)
            PlayerPrefs.SetInt("BestScore", currentScore);
    }


    //увеличить значение текущего счета на value
    public  void IncreaseScore(int value)
    {
        currentScore += value;
        //передача значение в отображающий текст
        currentScoreText.text = currentScore.ToString();
    }

    //сброс рекордов
    public void ResetScore()
    {

        //передача значения лучшего результата в отображающий текст
        bestScoreText.text = GetPreviousBestScore().ToString();

        //обнуление значения текущего счета
        currentScore = 0;

        //обновление счета на отображающем тексте
        currentScoreText.text = "0";
    }
}
