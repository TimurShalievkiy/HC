using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightOrder : MonoBehaviour
{
    public GameObject translation;

    public GameObject firstAnsver;
    public GameObject secondAnsver;
    public GameObject thirdAnsver;

    public GameObject firstButtonWithAnsver;
    public GameObject secondButtonWithAnsver;
    public GameObject thirdButtonWithAnsver;


    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        

        //InitTask2();
    }


   

    public void CheckAnsver(int buttonNum)
    {
      
        string ansver = "";

        if (buttonNum == 0)
        {
            ansver = firstButtonWithAnsver.transform.name;
            if(CheckWord(ansver))
                firstButtonWithAnsver.SetActive(false);
        }
        if (buttonNum == 1)
        {
            ansver = secondButtonWithAnsver.transform.name;
            if (CheckWord(ansver))
                secondButtonWithAnsver.SetActive(false);
        }
        if (buttonNum == 2)
        {
            ansver = thirdButtonWithAnsver.transform.name;
            if (CheckWord(ansver))
                thirdButtonWithAnsver.SetActive(false);
        }

        if (!firstButtonWithAnsver.activeSelf && !secondButtonWithAnsver.activeSelf && !thirdButtonWithAnsver.activeSelf)
        {
            Camera.main.transform.GetComponent<WordsManager>().IncrementDif(firstAnsver.transform.GetChild(0).GetComponent<Text>().text);
            Camera.main.transform.GetComponent<DataManager>().IncrementIndex();
            WordsManager.doTask = true;
            Destroy(transform.parent.gameObject);
        }
    }

    bool CheckWord(string ansver)
    {
        switch (counter)
        {
            case 0:
                if (firstAnsver.name == ansver)
                {

                    firstAnsver.GetComponent<Image>().color = Color.green;
                    firstAnsver.transform.GetChild(0).GetComponent<Text>().text = ansver;
                    counter++;
                    return true;
                }

                break;
            case 1:
                if (secondAnsver.name == ansver)
                {
                    secondAnsver.GetComponent<Image>().color = Color.green;
                    secondAnsver.transform.GetChild(0).GetComponent<Text>().text = ansver;
                    counter++;
                    return true;
                }


                break;
            case 2:
                if (thirdAnsver.name == ansver)
                {
                    thirdAnsver.GetComponent<Image>().color = Color.green;
                    thirdAnsver.transform.GetChild(0).GetComponent<Text>().text = ansver;
                    counter = 0;
                    return true;
                }
                break;

        }
        return false;
    }

    public void InitTask2(string str)
    {

       // Debug.Log("word = " + str);
        JsonData data = Camera.main.transform.GetComponent<DataManager>().GetByWord(str);
        //Camera.main.transform.GetComponent<DataManager>().IncrementIndex();

        translation = transform.GetChild(0).gameObject;
        firstAnsver = transform.GetChild(1).gameObject;
        secondAnsver = transform.GetChild(2).gameObject;
        thirdAnsver = transform.GetChild(3).gameObject;

        firstButtonWithAnsver = transform.GetChild(4).GetChild(0).gameObject;
        secondButtonWithAnsver = transform.GetChild(4).GetChild(1).gameObject;
        thirdButtonWithAnsver = transform.GetChild(4).GetChild(2).gameObject;

        //Debug.Log(data.translation);

       // Debug.Log(transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = data.translation;

 

        firstButtonWithAnsver.SetActive(true);
        secondButtonWithAnsver.SetActive(true);
        thirdButtonWithAnsver.SetActive(true);


        firstAnsver.name = data.first;
        firstAnsver.transform.GetChild(0).GetComponent<Text>().text = "VI";
        firstAnsver.GetComponent<Image>().color = Color.white;

        secondAnsver.name = data.second;
        secondAnsver.transform.GetChild(0).GetComponent<Text>().text = "VII";
        secondAnsver.GetComponent<Image>().color = Color.white;

        thirdAnsver.name = data.third;
        thirdAnsver.transform.GetChild(0).GetComponent<Text>().text = "VIII";
        thirdAnsver.GetComponent<Image>().color = Color.white;

        firstButtonWithAnsver.transform.GetChild(0).GetComponent<Text>().text = data.second;

        firstButtonWithAnsver.transform.name = data.second;

        secondButtonWithAnsver.transform.GetChild(0).GetComponent<Text>().text = data.first;
        secondButtonWithAnsver.transform.name = data.first;

        thirdButtonWithAnsver.transform.GetChild(0).GetComponent<Text>().text = data.third;
        thirdButtonWithAnsver.transform.name = data.third;

    }
    public IEnumerator InitTask()
    {
        yield return new WaitForSeconds(1);
        JsonData data = Camera.main.transform.GetComponent<DataManager>().GetNextWord();
        
        translation.transform.GetChild(0).GetComponent<Text>().text = data.translation;

        firstButtonWithAnsver.SetActive(true);
        secondButtonWithAnsver.SetActive(true);
        thirdButtonWithAnsver.SetActive(true);


        firstAnsver.name = data.first;
        firstAnsver.transform.GetChild(0).GetComponent<Text>().text = "VI";
        firstAnsver.GetComponent<Image>().color = Color.white;

        secondAnsver.name = data.second;
        secondAnsver.transform.GetChild(0).GetComponent<Text>().text = "VII";
        secondAnsver.GetComponent<Image>().color = Color.white;

        thirdAnsver.name = data.third;
        thirdAnsver.transform.GetChild(0).GetComponent<Text>().text = "VIII";
        thirdAnsver.GetComponent<Image>().color = Color.white;

        firstButtonWithAnsver.transform.GetChild(0).GetComponent<Text>().text = data.second;
        firstButtonWithAnsver.transform.name = data.second;

        secondButtonWithAnsver.transform.GetChild(0).GetComponent<Text>().text = data.first;
        secondButtonWithAnsver.transform.name = data.first;

        thirdButtonWithAnsver.transform.GetChild(0).GetComponent<Text>().text = data.third;
        thirdButtonWithAnsver.transform.name = data.third;

        StopCoroutine(InitTask());
    }
}
