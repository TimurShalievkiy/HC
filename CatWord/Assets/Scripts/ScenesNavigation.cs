using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScenesNavigation : MonoBehaviour
{
    public  void GotoGame()
    {
        SceneManager.LoadScene("Game");
    }
    public  void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
