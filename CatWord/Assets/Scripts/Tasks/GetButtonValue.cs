using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetButtonValue : MonoBehaviour
{
    public void GetVal()
    {
        if(transform.parent.parent.GetComponent<WriteByTrafAndTransl>().CheckAns(transform.GetChild(0).GetComponent<Text>().text))
        {
            gameObject.SetActive(false);
        }
    }
}
