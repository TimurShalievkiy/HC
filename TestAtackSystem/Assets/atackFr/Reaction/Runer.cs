using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runer : MonoBehaviour
{
    bool startRun = false;
    [SerializeField] AtackListener atackListener;
    private void Start()
    {
        if (atackListener == null)
            atackListener = transform.GetComponent<AtackListener>();


        atackListener.OnAtacked += delegate { StartRun(); };
    }
    void Update()
    {
        if (startRun)
        {
            transform.position += transform.forward * 5f * Time.deltaTime;
            transform.Rotate(0, 1f, 0);

        }
    }

    void StartRun()
    {
        startRun = true;
    }
}
