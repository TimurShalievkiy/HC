using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class connecting : MonoBehaviour
{
    [SerializeField]Text text;
    [SerializeField] float delay;
    float currentDelay;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Loading";
        currentDelay = delay;
    }

    // Update is called once per frame
    void Update()
    {
        currentDelay -= Time.deltaTime;
        if (currentDelay <= 0)
        {
            if (text.text.Length > 9)
                text.text = "Loading";
            text.text += ".";
            currentDelay = delay;
        }
    }
}
