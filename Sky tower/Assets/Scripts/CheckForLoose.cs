using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckForLoose : MonoBehaviour
{
    
   public static bool loose = false;
   // [SerializeField] Transform startPlace;
    [SerializeField] GameObject loosePanel;
    [SerializeField] GameObject pauseButton;
    // Update is called once per frame
    private void Start()
    {
        loose = false;
    }
    void Update()
    {
        if(!loose&& CraneController.instance)
        for (int i = 0; i < CraneController.instance.listOfBlocks.Count; i++)
        {
                if (i == 0)
                {

                    if (GameController.instance.house.transform.position.y >= CraneController.instance.listOfBlocks[0].transform.position.y)
                    {
                        Loose();
                    }
                    if (CraneController.instance.listOfBlocks[0].transform.rotation.z > 45 || CraneController.instance.listOfBlocks[0].transform.rotation.z <-45)
                    {
                        Loose();
                    }
                }
                else
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (CraneController.instance.listOfBlocks[j].transform.position.y >= CraneController.instance.listOfBlocks[i].transform.position.y)
                        {
                            Loose();
                        }

                        if (CraneController.instance.listOfBlocks[j].transform.rotation.z > 0.3 || CraneController.instance.listOfBlocks[j].transform.rotation.z < -0.3)
                        {
                            Loose();
                        }
                    }
                   
                }
        }

    }
    void Loose()
    {
        loose = true;
        Debug.Log("loose1");
        loosePanel.SetActive(true);
        pauseButton.SetActive(false);
    }
}
