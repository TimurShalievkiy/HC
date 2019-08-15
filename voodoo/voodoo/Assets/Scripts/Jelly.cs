using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{

    [SerializeField] Transform _endOfShadow;
    [SerializeField] Transform _mainObject;


    static Transform endOfShadow;
    static Transform mainObject;

    private void Start()
    {
        endOfShadow = _endOfShadow;
        mainObject = _mainObject;


    }
    public static void SetNewPos(Vector3 v)
    {
        endOfShadow.gameObject.SetActive(true);
        v.y = mainObject.position.y;
        endOfShadow.transform.position = v;
    }

}
