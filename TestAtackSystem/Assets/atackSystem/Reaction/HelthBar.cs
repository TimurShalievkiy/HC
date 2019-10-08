using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelthBar : MonoBehaviour
{
    // отображение и уменьшение жизней
    [SerializeField] Slider heltbar;

    [SerializeField] AtackListener atackListener;

    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        if (atackListener == null)
            atackListener = transform.GetComponent<AtackListener>();

        if (heltbar == null)
        {
            GameObject g = Resources.Load<GameObject>("HeltBar");
            g = Instantiate(g, transform);
            heltbar = g.GetComponentInChildren<Slider>();
        }

        //подпись на событие обьект атакован
        atackListener.OnAtacked += delegate { GetDamage(); };
    }

    void GetDamage()
    {
        heltbar.value -= 10;
        if (heltbar.value <= 0)
            Destroy(gameObject);
    }
}
