using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static int score = 0;
    public Text text;
    private void Start()
    {
        GameObject g = GameObject.Find("ScoreManager");
        if (g != this.gameObject)
        {
            Destroy(g);
            score = 0;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        if (text != null)
            text.text = score.ToString();
    }

}
