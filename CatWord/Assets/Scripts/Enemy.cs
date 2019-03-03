using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static int counter = 2;
    public float helth;
    public float damage;
    public float atackDuration;
    public Slider helthBar;
    

    // Start is called before the first frame update
    void Start()
    {
        helthBar.maxValue = helth;
    }


    public void GetDamage(float dam)
    {
        helthBar.value -= dam;
        if (helthBar.value <= 0)
        {
            counter++;
            if (counter == 3)
            {
                WordsManager.doTask = false;
            Camera.main.GetComponent<WordsManager>().CheckForAddingNewWord();
                counter = 0;
            }
            GameObject.Destroy(gameObject);


        }
    }
    public float DoDamage()
    {
        return damage;
    }
}
