using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public static bool move = true;
    public GameObject back;
    public GameProcess gameProcess;
    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            back.transform.position += Vector3.left;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.GetComponent<Enemy>())
        {
           // Debug.Log(collision.transform.name);
            move = false;
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            gameProcess.enemy = enemy;
            //Debug.Log(false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Enemy>())
        {
           // Debug.Log(collision.transform.name + " true");
            move = true;
            transform.GetComponent<Cat>().ResetHelth();
            //Debug.Log(true);
        }
    }
}
