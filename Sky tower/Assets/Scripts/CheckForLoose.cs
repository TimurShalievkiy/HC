using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckForLoose : MonoBehaviour
{
    
   public static bool loose = false;
    [SerializeField] Transform startPlace;
    [SerializeField] GameObject loosePanel;
    // Update is called once per frame
    private void Start()
    {
        loose = false;
    }
    void Update()
    {
        if(!loose)
        for (int i = 0; i < CraneController.instance.listOfBlocks.Count; i++)
        {
                if (i == 0)
                {

                    if (startPlace.position.y >= CraneController.instance.listOfBlocks[0].transform.position.y)
                    {
                        loose = true;
                        Debug.Log("loose1");
                        loosePanel.SetActive(true);
                    }

                }
                else
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (CraneController.instance.listOfBlocks[j].transform.position.y >= CraneController.instance.listOfBlocks[i].transform.position.y)
                        {
                            loose = true;
                            Debug.Log("loose1");
                            loosePanel.SetActive(true);
                        }
                    }
                   
                }
        }

    }
}
