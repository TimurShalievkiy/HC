using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoosScreen : MonoBehaviour {

    public Text t;
	// Use this for initialization
	void Start () {
        t.text = ScoreManager.score.ToString();
	}
	

}
