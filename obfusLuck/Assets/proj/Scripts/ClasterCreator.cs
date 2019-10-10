using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClasterCreator : MonoBehaviour {


    public List<GameObject> ListOfClasters;

    public static int counterOfClasters = 0;


    public int index = 0;

  
    public void CreateNewClaster()
    {
        if (counterOfClasters == 0)
        {
            GameObject G = Instantiate(ListOfClasters[index], transform.position, Quaternion.identity);
            G.transform.parent = this.transform;
            G.transform.GetComponent<Claster>().speedOfClaster = JsonFileWriter.jsondata.speedOfClaster * 10;
            counterOfClasters = 1;
            index++;

            if (index == ListOfClasters.Count)
                index = 0;
        }
    }

    private void Start()
    {
        ClasterCreator.counterOfClasters = 0;
    }
    private void Update()
    {
        CreateNewClaster();
    }
}
