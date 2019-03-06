using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    //номер текущей сцены нужен для запуска карутины с таймером прелоудера
    int currentSceneIndex;

    //время задержки прелоудера
    float timeToWait = 5f;


    // Start is called before the first frame update
    void Start()
    {
        //получение значения номера текущей сцены
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //если номер == 0 то запускаем карутину с таймером
        if (currentSceneIndex == 0)
        {
            StartCoroutine(WaitForTime());
        }
    }

    // метод карутины
    IEnumerator WaitForTime()
    {
        //ожидаем заданое время
        yield return new WaitForSeconds(timeToWait);
        //загружаем сцену лобби
        LoaaLobbiScene();
    }

    //загрузка сцены лобби 
    public void LoaaLobbiScene()
    {
        SceneManager.LoadScene("Lobbi");
    }

    //загрузка сцены с игровым процессом
    public void LoaaMainGameScene()
    {
        SceneManager.LoadScene("MainGame");
    }
}
