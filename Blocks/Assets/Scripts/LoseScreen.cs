using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    public Text currentScore;
    public Text bestScore;
    public Text stars;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame

    public void SetLoseScreenValue()
    {
        currentScore.text = "Score: " + ScoreManager.currentScore.ToString();
        bestScore.text ="Best: " +  ScoreManager.GetPreviousBestScore().ToString();
        stars.text = "Stars: " + 0;
        if(ScoreManager.currentScore != ScoreManager.GetPreviousBestScore())
        ScoreManager.SaveResult();

    }
}
