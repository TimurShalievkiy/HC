using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{

    [SerializeField] Transform _endOfShadow;
    [SerializeField] Transform _mainObject;
    [SerializeField] Transform _conteiner;

    public static Transform endOfShadow;
    static Transform mainObject;
    static Transform conteiner;
    public static List<Transform> shadowTrigers;


    private void Start()
    {
        shadowTrigers = new List<Transform>();
        endOfShadow = _endOfShadow;
        mainObject = _mainObject;
        conteiner = _conteiner;

    }
    public static void SetNewPos(Vector3 v, bool flag)
    {
        if (flag)
        {

            if (!endOfShadow.gameObject.activeSelf)
                endOfShadow.gameObject.SetActive(true);



            v.y = conteiner.position.y;
            endOfShadow.transform.position = v;



            endOfShadow.localScale = conteiner.localScale;

            for (int i = 0; i < shadowTrigers.Count; i++)
            {
                if (shadowTrigers[i] != null)
                {
                    shadowTrigers[i].localScale = new Vector3(conteiner.localScale.x, conteiner.localScale.y, shadowTrigers[i].localScale.z);
                    shadowTrigers[i].parent.position = new Vector3(shadowTrigers[i].parent.position.x, mainObject.position.y, shadowTrigers[i].parent.position.z);
                }
            }
        }
    }

}
