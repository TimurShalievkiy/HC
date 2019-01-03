using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClasterCreator : MonoBehaviour {

    //список префабов кластеров
    public List<GameObject> clasters;
    //счетчик кластеров если больше 0 то кластер не создастся
    public static int counter = 0;

    //индекс для смены порядка кластеров
    public int index = 0;

    private void Start()
    {
        //инициализируем значение
        ClasterCreator.counter = 0;
    }
    private void Update()
    {
        //вызываем метод создания кластеров
        CreateClaster();
    }
    public void CreateClaster()
    {
        //если количество кластеров = 0
        if (counter == 0)
        {
            //создаем обьект из списка префабов по индексу
            GameObject G = Instantiate(clasters[index], transform.position, Quaternion.identity);

            //выставляем позицию обьету
            G.transform.parent = this.transform;

            //выставляем скорость
            G.transform.GetComponent<Claster>().speed = JsonFileWriter.data.clasterSpeed * 10;

            //увеличиваем количество кластеров
            counter = 1;

            //инкремент индекса для смены кластеров
            index++;
            // если индекс равен количеству элементов в списке
            if (index == clasters.Count)
                //то выставляем в 0
                index = 0;
        }
    }
}
