using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestructionZone : MonoBehaviour {

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.transform.GetComponent<Ball>() != null)
        {
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        }
        if (coll.transform.GetComponent<Claster>() != null)
        {
            ClasterCreator.counterOfClasters = 0;
            Destroy(coll.gameObject);
        }
    }

}
