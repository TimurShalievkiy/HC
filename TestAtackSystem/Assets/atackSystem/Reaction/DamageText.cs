using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    //вывод текста урона
    [SerializeField] Text damageText;

    [SerializeField] AtackListener atackListener;

    Vector3 stertPos;

    bool startAnim = false;
    float counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (atackListener == null)
            atackListener = transform.GetComponent<AtackListener>();

        if (damageText == null)
        {
            GameObject g = Resources.Load<GameObject>("DamageText");
            g = Instantiate(g, transform);
            damageText = g.GetComponentInChildren<Text>();
            damageText.text = "10";
        }
        stertPos = damageText.transform.position;

        //подпись на событие обьект атакован
        atackListener.OnAtacked += delegate { GetDamage(); };
    }

    void GetDamage()
    {
        damageText.transform.position = stertPos;
        damageText.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        startAnim = true;
        counter = 0;
    }

    private void Update()
    {
        if (startAnim)
        {

            Debug.Log(1);
            if (damageText.transform.localScale.x < 1)
                damageText.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);

            damageText.transform.position += new Vector3(0, 0.05f, 0);
            counter += Time.deltaTime;

            if (counter >= 1)
            {
                damageText.transform.position = stertPos;
                damageText.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                startAnim = false;
            }
        }
        
    }


}
