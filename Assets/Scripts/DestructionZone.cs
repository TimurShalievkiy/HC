using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestructionZone : MonoBehaviour {

    //проверка входящего в тригер
    private void OnTriggerEnter(Collider other)
    {
        //если в тригер войдет кластер
        if (other.transform.GetComponent<Claster>() != null)
        {
            //щетчик кластеров выствляем в 0 для создание нового кластреа
            ClasterCreator.counter = 0;
            //удаляем кластер
            Destroy(other.gameObject);
        }
        //если в тригер вошла шайба
        if (other.transform.GetComponent<Ball>() != null)
        {
            //вызвать сцену проиграша
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        }        
    }

}
