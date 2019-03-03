using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cat : MonoBehaviour
{

    public float helth;
    public float damage;
    public Slider helthBar;
    public float atackDuration;
    // Start is called before the first frame update
    private void Start()
    {
        helthBar.maxValue = helth;
    }
    public  void GetDamage(float dam)
    {
       helthBar.value -= dam;
        if (helthBar.value <= 0)
        {
            if(gameObject)
            GameObject.Destroy(gameObject);

        }
    }
    public  float DoDamage()
    {
       // Debug.Log(transform.GetComponent<Animator>().name);
        
        return damage;
    }
    public void ResetHelth()
    {
        helthBar.value = helth;
    }
}

