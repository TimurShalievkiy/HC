using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WordsManager : MonoBehaviour
{
   public static List<Word> words;
    public GameObject newWordPanel;

    public static bool doTask = false;

    private void Start()
    {
        words = new List<Word>();
    }

    private void Update()
    {
        if (doTask)
        {
           // Debug.Log("Create task");
            int min = words.Min(y => y.dif);
            AddTask(words.Find(x => x.dif == min));
            doTask = false;
        }
    }
    public static void AddNewWordToList(Word w)
    {
        if (!words.Exists(x => x.word ==w.word))
            words.Add(w);
    }

    public static void AddNewWordToList(JsonData w)
    {  
        if (!words.Exists(x => x.word == w.first))
            words.Add(new Word( w.first,1));

        //if (!words.Exists(x => x.word == w.second))
        //    words.Add(new Word(w.second, 1));

        //if (!words.Exists(x => x.word == w.third))
        //    words.Add(new Word(w.third, 1));
    }
    public void CheckForAddingNewWord()
    {
        if (words.Count == 0)
        {

            AddNewWordToList(transform.GetComponent<DataManager>().GetNextWord());
            
            SetNewWordPanel(transform.GetComponent<DataManager>().GetNextWord());
            DataManager.index++;
            doTask = true;
           
           // Debug.Log(words.Count);
        }

        else if (words.Count(x => x.dif == 0) == 0)
        {
            AddNewWordToList(transform.GetComponent<DataManager>().GetNextWord());
            SetNewWordPanel(transform.GetComponent<DataManager>().GetNextWord());
            DataManager.index++;
            int min = words.Min(y => y.dif);
        }
    }
    public void IncrementDif(string word)
    {
        for (int i = 0; i < words.Count; i++)
        {
            if (words[i].word == word)
            {
                //Debug.Log(words[i].dif + " " + words[i].word);
                words[i].dif++;
                //Debug.Log(words[i].dif);
            }

        }
    }
    public void SetNewWordPanel(JsonData jd)
    {
        //Debug.Log(jd.first);
        newWordPanel.SetActive(true);
        newWordPanel.transform.GetChild(1).GetComponent<Text>().text = jd.translation + "\n";
        newWordPanel.transform.GetChild(1).GetComponent<Text>().text += jd.first + "\n";
        newWordPanel.transform.GetChild(1).GetComponent<Text>().text += jd.second + "\n";
        newWordPanel.transform.GetChild(1).GetComponent<Text>().text += jd.third ;
     
    }
    public void AddTask(Word x)
    {
       
        switch (x.dif)
        {
            case 1:
                GameObject g = Instantiate(Resources.Load("Task/TaskPanel") as GameObject, Camera.main.transform.GetChild(0));
                g.transform.GetChild(0).GetComponent<RightOrder>().InitTask2(x.word);
                break;
            case 2:
                GameObject g2 = Instantiate(Resources.Load("Task/TaskByTrafAndTransl") as GameObject, Camera.main.transform.GetChild(0));
                g2.transform.GetChild(0).GetComponent<WriteByTrafAndTransl>().InitTask(x.word);
                break;

        }
        
    }
}


public class Word
{
    public string word;
    public int dif;

    public Word(string word, int dif)
    {
        this.word = word;
        this.dif = dif;
    }
}
