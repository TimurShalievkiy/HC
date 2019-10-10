using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Box : MonoBehaviour {

   
    public int valueOfScore = 1;



   
    private void OnCollisionEnter(Collision collision)
    {
      
        if (collision.transform.GetComponent<Ball>() != null)
        {       
            ScoreManager.PlayerScore += valueOfScore;

            
            if (GetComponent<Animator>() != null)
            {              
                GetComponent<BoxCollider>().enabled = false;              
                GetComponent<Animator>().SetInteger("indexAnim", Random.Range(1,3));          
                Destroy(this.gameObject, 1);
            }

           
            if (this.transform.parent.GetComponent<Claster>() == null)
            {               
                Destroy(this.transform.parent.gameObject, 1);
            }
        }



       
        if (collision.transform.GetComponent<RocketController>() != null)
        {         
            Destroy(collision.gameObject,1);          
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings-1);
        }
    }
    
}
