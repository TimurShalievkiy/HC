using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackListener : MonoBehaviour
{
//этот клас вешается как основной на обьект у которого должны быть разные рекции
// все скрипты из папки Reaction автоматически берут  или если нужно назначить на прямую имеют ссылку на этот компонент
    AtackListener instance;

    public delegate void IsAtaked();
    //событие которое вызываем когда обьект атакован
    public  event IsAtaked OnAtacked;

    private void Awake()
    {
        instance = this;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.tag == "Weapon")
        {
            //если игрок сейчас атакует
            if (PlayerAtackController.isAtacking)
            {
                PlayerAtackController.isAtacking = false;
                //вызываем событие и все подписанные методы
                if (OnAtacked != null)
                    OnAtacked();

                Debug.Log("is ataked");
            }
        }
    }
}
