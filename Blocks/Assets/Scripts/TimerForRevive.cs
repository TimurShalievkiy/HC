using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerForRevive : MonoBehaviour
{
    public GameObject loseScreen;
    public Image progresImage;
    public Text timerText;
    public float duration = 5;
    float currentDuration;
     float percent = 0;
    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    public void ResetTimer()
    {
        currentDuration = duration;
        progresImage.fillAmount = 1;
        timerText.text = duration.ToString();
        percent = duration / 100;
    }
    void Update()
    {
        if (currentDuration >= 0)
        {
            currentDuration -= Time.deltaTime;
            if (currentDuration > 0)
                timerText.text = ((int)currentDuration + 1).ToString();
            else
                timerText.text = ((int)currentDuration).ToString();

            progresImage.fillAmount = (currentDuration / percent) / 100;
        }
        else {
            loseScreen.GetComponent<LoseScreen>().SetLoseScreenValue();
            loseScreen.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
