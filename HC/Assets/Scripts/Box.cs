using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Box : MonoBehaviour {

    //количество очков в счетчик
    public int value = 1;

    //проверка с чем соприкасается блок
    private void OnCollisionEnter(Collision collision)
    {
        //если соприкасается с шайбой
        if (collision.transform.GetComponent<Ball>() != null)
        {
            //увеличить счет
            ScoreManager.score += value;

            //проверка на наличие аниматора
            if (GetComponent<Animator>() != null)
            {
                //отключаем колайдер для устранения ложных ударений
                GetComponent<BoxCollider>().enabled = false;

                //выбираем какая анимация воспроизведется
                GetComponent<Animator>().SetInteger("indexAnim", Random.Range(1,3));

                //удаляем блок
                Destroy(this.gameObject, 1);
            }

            //если это вложеный обьект а верхний не является кластером
            if (this.transform.parent.GetComponent<Claster>() == null)
            {
                //то удаляем родителя
                Destroy(this.transform.parent.gameObject, 1);
            }
        }

        //если соприкасается с ракеткой
        if (collision.transform.GetComponent<RocketController>() != null)
        {
            //удаляем ракетку
            Destroy(collision.gameObject,1);
            //загрузка сцены проиграша 
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings-1);
        }
    }
    
}
