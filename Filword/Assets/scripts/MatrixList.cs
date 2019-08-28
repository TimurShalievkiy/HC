using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MatrixList : MonoBehaviour
{
    
    List<Matrix> list ;

    private void Awake()
    {
        list = new List<Matrix>();

        list.Add(new Matrix(new int[,] {
            { 1,1},
            { 1,1}
        }, new List<List<int>>() { new List<int>() {0,1,2,3 } }));

        list.Add(new Matrix(new int[,] {
            { 2,1,1},
            { 2,2,1},
            { 2,2,1}
        }, new List<List<int>>() { new List<int>() { 0,3,4,7,6},
                                   new List<int>(){ 1,2,5,8} }));

        
    }
}
