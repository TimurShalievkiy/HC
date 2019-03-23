using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcessManager : MonoBehaviour
{
    [SerializeField] TouchZonesCreator creator;
    [SerializeField] PllayingAlong pllayingAlong;

    //// Update is called once per frame
    //void Update()
    //{
    //    //if (creator.transform.childCount == 0)
    //    //{
    //    //    if (FieldCondition.GetCountOfFreeCell(FieldManager.GetCurrentFieldState()) <= 45)
    //    //        GenerateWaveWithPlayingAlong();
    //    //    else
    //    //        GenerateRandomWave();
    //    //}
    //}
    public void GenerateWaveAfterRevive()
    {
        creator.GenerateNewWaveOfShape(new int[] { 0,0,0});
        //Debug.Log("GenerateWaveAfterRevive()");
    }

    public void GenerateWaveWithPlayingAlong()
    {
        pllayingAlong.GetShapesWithHelp();
       // Debug.Log("GenerateWaveWithPlayingAlong()");
    }
    public void GenerateRandomWave()
    {
        creator.GenerateNewWaveOfShape();
       // Debug.Log("GenerateNewWaveOfShape()");
    }
}
