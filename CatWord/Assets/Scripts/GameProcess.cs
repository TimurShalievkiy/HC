using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcess : MonoBehaviour
{
    public Cat cat;
    public Enemy enemy;
    public float currentCAtAtackDuration;
    public float currentEnemyAtackDuration;
    // Start is called before the first frame update
    void Start()
    {
        currentCAtAtackDuration = cat.atackDuration;
        currentEnemyAtackDuration = enemy.atackDuration;
    }

    // Update is called once per frame
    void Update()
    {
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
}
