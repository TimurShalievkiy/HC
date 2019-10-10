using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static int PlayerScore = 0;
    public Text textOfScore;

    private void Update()
    {
        if (textOfScore != null)
            textOfScore.text = PlayerScore.ToString();
    }
    private void Start()
    {
        GameObject g = GameObject.Find("ScoreManager");
        if (g != this.gameObject)
        {
            Destroy(g);
            PlayerScore = 0;
        }
        DontDestroyOnLoad(this.gameObject);
    }

}
