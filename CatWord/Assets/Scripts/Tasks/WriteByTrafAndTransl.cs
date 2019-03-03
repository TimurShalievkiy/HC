using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WriteByTrafAndTransl : MonoBehaviour
{
    public GameObject translation;
    public string word2 = "";
    public static int count = 0;
    // Start is called before the first frame update
    void Start()
    {
       // InitTask();
    }



    public void InitTask(string str)
    {
        word2 = str;
        //Debug.Log(str + " 22222222222222");
        JsonData data = Camera.main.transform.GetComponent<DataManager>().GetByWord(str);
        //Camera.main.transform.GetComponent<DataManager>().IncrementIndex();
        translation.transform.GetChild(0).GetComponent<Text>().text = data.translation;

        string word = data.first;
        GameObject g;
        for (int i = 0; i < word.Length; i++)
        {
            g = GameObject.Instantiate(Resources.Load("Task/leter") as GameObject, transform.position, Quaternion.identity);
            g.transform.GetChild(0).GetComponent<Text>().text = word[i].ToString();
            g.transform.GetChild(0).name = word[i].ToString();
            g.transform.GetChild(0).GetComponent<Text>().color = new Color(g.transform.GetChild(0).GetComponent<Text>().color.r, g.transform.GetChild(0).GetComponent<Text>().color.g, g.transform.GetChild(0).GetComponent<Text>().color.b, 0.5f);
            g.transform.parent = transform.GetChild(1);
            g.transform.localScale = new Vector3(1, 1, 1);
        }

        List<string> ls = new List<string>();
        for (int i = 0; i < word.Length; i++)
        {
            ls.Add(word[i].ToString());
        }

        for (int i = ls.Count - 1; i >= 1; i--)
        {
            int j = Random.Range(0, ls.Count);
            // обменять значения data[j] и data[i]
            var temp = ls[j];
            ls[j] = ls[i];
            ls[i] = temp;
        }

        word = "";
        for (int i = 0; i < ls.Count; i++)
        {
            word += ls[i];
        }
        for (int i = 0; i < word.Length; i++)
        {
            g = GameObject.Instantiate(Resources.Load("Task/letterButton") as GameObject, transform.position, Quaternion.identity);
            g.transform.GetChild(0).GetComponent<Text>().text = word[i].ToString();

            g.transform.GetChild(0).name = word[i].ToString();


            g.transform.parent = transform.GetChild(2);
            g.transform.localScale = new Vector3(1, 1, 1);
        }
    }


    public bool CheckAns(string str)
    {
 
        if (transform.GetChild(1).GetChild(count).GetChild(0).GetComponent<Text>().text == str)
        {
            transform.GetChild(1).GetChild(count).GetChild(0).GetComponent<Text>().color = Color.green;
            count += 1;
            //Debug.Log(" 2 " + count);
            if (count == transform.GetChild(1).transform.childCount)
            {
                count = 0;

                
                WordsManager.doTask = true;
                ClearTask();
                //Destroy(gameObject.transform.parent);
                //InitTask();
            }
            return true;
        }
        return false;
    }
    void ClearTask()
    {
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        Camera.main.transform.GetComponent<WordsManager>().IncrementDif(word2);
        //
    }

}
