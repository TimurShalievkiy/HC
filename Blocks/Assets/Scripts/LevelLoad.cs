using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{

    int currentSceneIndex;
    float timeToWait = 3f;
    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            StartCoroutine(WaitForTime());
        }
    }


    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait);
        LoaaLobbiScene();
    }
    // Update is called once per frame


    public void LoaaLobbiScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void LoaaMainGameScene()
    {
        SceneManager.LoadScene("MainGame");
    }
}
