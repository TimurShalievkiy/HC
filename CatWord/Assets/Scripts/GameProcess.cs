using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcess : MonoBehaviour
{
    public Cat cat;
    public Enemy enemy;
    public float currentCAtAtackDuration;
    public float currentEnemyAtackDuration;
    public GameObject doubleDamage;
    public float timeDiubledamage = 5;
    public float currentTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        currentCAtAtackDuration = cat.atackDuration;
        currentEnemyAtackDuration = enemy.atackDuration;
    }

    // Update is called once per frame
    void Update()
    //{
    //    if (currentTime >= 0)
    //    {
    //        currentTime -= Time.deltaTime;
    //        if (!doubleDamage.active)
    //        {
    //            doubleDamage.SetActive(true);
    //            DoDamage();
    //        }

    //    }
    //    else {
    //        if(!doubleDamage.active)
    //            doubleDamage.SetActive(false);
    //        currentTime = timeDiubledamage;
    //    }
        Damage();
    }
    public void Damage()
    {
        if (!MoveScript.move)
        {
            if (currentEnemyAtackDuration <= 0)
            {
                if (cat != null)
                {
                    currentEnemyAtackDuration = enemy.atackDuration;
                    
                    cat.GetDamage(enemy.damage);
                }
            }
            else
            {
                currentEnemyAtackDuration -= Time.deltaTime;
            }


            if (currentCAtAtackDuration <= 0)
            {
                if (enemy != null)
                {
                    //Debug.Log(111111111111111);
                    
                    enemy.GetDamage(cat.damage);
                    currentCAtAtackDuration = cat.atackDuration;
                }
                
                   
                
            }
            else
            {
                currentCAtAtackDuration -= Time.deltaTime;
            }
        }
    }

    public void DoDamage()
    {
        
        if (!MoveScript.move)
        {

            if (enemy != null)
            {
                Debug.Log("Damage");
                enemy.GetDamage(cat.damage / 2);
                currentCAtAtackDuration = cat.atackDuration;

            }
            else {
                Debug.Log("No Damage");
            }

            }
       
        
    }

    public void DoubleDamage()
    {
        Debug.Log("123");
    }
}
