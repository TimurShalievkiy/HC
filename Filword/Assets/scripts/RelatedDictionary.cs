using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RelatedDictionary : MonoBehaviour
{


    List<string> listWords;
    List<CountOfWordsAndLength> listCounter;
    List<string> passedWords;

    #region initialisation
    // Start is called before the first frame update


    public void InitDictionary(string name, List<string> ls)
    {
        InitListWords(ls);
        InitPassedWords(name);
        InitListCounter();
       
    }

  
    void InitListWords(List<string> ls)
    {
        listWords = new List<string>();

        for (int i = 0; i < ls.Count; i++)
        {
            listWords.Add(ls[i]);
        }

    }

    void InitPassedWords(string name)
    {
        passedWords = new List<string>();

        if (PlayerPrefs.HasKey(name))
        {
            string[] arr = PlayerPrefs.GetString(name).Split('#');
            for (int i = 0; i < arr.Length; i++)
            {
                passedWords.Add(arr[i]);
            }
        }
    }

    void InitListCounter()
    {
        listCounter = new List<CountOfWordsAndLength>();
        int length;
        for (int i = 0; i < listWords.Count; i++)
        {
            if (!passedWords.Exists(x => x == listWords[i]))
            {
                length = listWords[i].Length;
                if (!listCounter.Exists(x => x.countOfLetters == length))
                    listCounter.Add(new CountOfWordsAndLength(length, 1));
                else
                    listCounter.Find(x => x.countOfLetters == length).countOfWords++;
            }
        }

    }

    #endregion initialisation

}


class CountOfWordsAndLength {
   public int countOfLetters;
    public int countOfWords;

    public CountOfWordsAndLength(int countOfLetters, int countOfWords)
    {
        this.countOfLetters = countOfLetters;
        this.countOfWords = countOfWords;
    }
}
