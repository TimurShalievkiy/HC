using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoosScreen : MonoBehaviour {

    public Text t1;

	void Start () {
        t1.text = ScoreManager.PlayerScore.ToString();
	}
	

}
