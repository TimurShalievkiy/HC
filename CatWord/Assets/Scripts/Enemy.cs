using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float helth;
    public float damage;
    public Slider helthBar;

    public 

    // Start is called before the first frame update
    void Start()
    {
        helthBar.maxValue = helth;
    }

    // Update is called once per frame
    void Update()
    {
        if (helthBar.value > 0)
        {
            helthBar.value -= Time.deltaTime;
        }
        else
            GameObject.Destroy(gameObject);
    }
    public void GetDamage(float dam)
    {
        helth -= dam;
    }
    public float DoDamage()
    {
        return damage;
    }
}
