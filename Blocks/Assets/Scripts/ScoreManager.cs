using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int currentScore = 0;
    public Text bestScoreText;
    public Text currentScoreText;

    // Start is called before the first frame update
    void Start()
    {
        ResetScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int GetPreviousBestScore()
    {
        int score = 0;
        if (PlayerPrefs.HasKey("BestScore"))
        {
            score = PlayerPrefs.GetInt("BestScore");
        }
        return score;
    }
    public static void SaveResult()
    {
        if (GetPreviousBestScore() < currentScore)
            PlayerPrefs.SetInt("BestScore", currentScore);
    }

    public  void IncreaseScore(int value)
    {
        currentScore += value;
        currentScoreText.text = currentScore.ToString();
    }
    public void ResetScore()
    {
        bestScoreText.text = GetPreviousBestScore().ToString();
        currentScore = 0;
        currentScoreText.text = "0";
    }
}
