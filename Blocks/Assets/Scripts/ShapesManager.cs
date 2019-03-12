using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesManager : MonoBehaviour
{


    public static List<int[,]> GetAllShapes()
    {
        List<int[,]> lsh = new List<int[,]>();

        lsh.Add(new int[,] { { 0,0,0,0,0},
                             { 0,0,0,0,0},
                             { 0,0,1,0,0},
                             { 0,0,0,0,0},
                             { 0,0,0,0,0} });

        lsh.Add(new int[,] { { 0,0,0,0,0},
                             { 0,0,0,0,0},
                             { 0,1,1,1,0},
                             { 0,0,0,0,0},
                             { 0,0,0,0,0} });

        lsh.Add(new int[,] { { 0,0,0,0,0},
                             { 0,0,1,0,0},
                             { 0,0,1,0,0},
                             { 0,0,1,0,0},
                             { 0,0,0,0,0} });

        lsh.Add(new int[,] { { 0,0,0,0,0},
                             { 0,1,1,0,0},
                             { 0,0,1,1,0},
                             { 0,0,0,0,0},
                             { 0,0,0,0,0} });

        lsh.Add(new int[,] { { 0,0,0,0,0},
                             { 0,0,0,0,0},
                             { 1,1,1,1,1},
                             { 0,0,0,0,0},
                             { 0,0,0,0,0} });

        return lsh;

    }
}
