using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        //если соприкасается с блоком
        if (collision.transform.GetComponent<Box>() != null)
        {
            //если у соприкоснувшегося обьекта родитель родителя имеет компонент кластер
            if (collision.transform.parent.transform.parent.GetComponent<Claster>() != null)
                //то выставляем нормальную скорость
                collision.transform.parent.transform.parent.GetComponent<Claster>().speed = JsonFileWriter.data.clasterSpeed;
            //если у соприкоснувшегося обьекта родитель имеет компонент
            else if (collision.transform.parent.GetComponent<Claster>() != null)
            {
                //то выставляем нормальную скорость
                collision.transform.parent.GetComponent<Claster>().speed = JsonFileWriter.data.clasterSpeed;
            }
        }

    }
}
