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
            { 1,1,},
            { 1,1}
        }));

        list.Add(new Matrix(new int[,] {
            { 2,1,1},
            { 2,2,1},
            { 2,2,1}
        }));

        
    }
}
