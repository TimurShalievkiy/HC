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
        // Debug.Log(value);

        if (value == 20)
        {
            currentScore += 30;
            //Debug.Log("+30");
            currentScoreText.text = currentScore.ToString();
        }
        else if (value == 30)
        {
            currentScore += 50;
           // Debug.Log("+50");
            currentScoreText.text = currentScore.ToString();
        }
        else if (value == 40)
        {
            currentScore += 80;
           // Debug.Log("+80");
            currentScoreText.text = currentScore.ToString();
        }
        else if (value == 50)
        {
            currentScore += 120;
            //Debug.Log("+120");
            currentScoreText.text = currentScore.ToString();
        }
        //else if (FieldCondition.GetCountOfFreeCell(FieldManager.GetCurrentFieldState())  == 100 && value >=20)
        //{
        //    currentScore += 240;
        //    Debug.Log("+240");
        //    currentScoreText.text = currentScore.ToString();
        //}
        else
        {
            //Debug.Log("+" + value);

            currentScore += value;
            currentScoreText.text = currentScore.ToString();
        }

      
        //передача значение в отображающий текст
        
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
