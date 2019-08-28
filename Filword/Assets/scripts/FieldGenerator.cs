using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldGenerator : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject cell;
    [SerializeField] GridLayoutGroup grid;
    [SerializeField] int xRange;
    [SerializeField] int yRange;
    [SerializeField] List<GameObject> cells;

    int x = 2;
    int y = 2;

    public void GenerateGameField()
    {
        cells = new List<GameObject>();

        xRange = x;
        yRange = y;

        grid.constraintCount = xRange;
        float canvasHeight = canvas.GetComponent<RectTransform>().sizeDelta.x;
        float canvasWidth = canvas.GetComponent<RectTransform>().sizeDelta.y;
        float height = (canvasHeight - grid.spacing.x*xRange - grid.spacing.x * 2) /xRange ;

        Debug.Log(height+ " " +(height * yRange + grid.spacing.x*2 ) + " - " + (canvasWidth * 0.8f));

        if (height * yRange + grid.spacing.x*2  > canvasWidth * 0.8f )
        {
            Debug.Log("123");
            height = (canvasHeight * 0.8f - grid.spacing.x * yRange - grid.spacing.x * 2) / yRange;
        }

       // Debug.Log(canvas.GetComponent<RectTransform>().sizeDelta.x + " " + Screen.height / xRange);
        grid.cellSize = new Vector2(height, height);


        for (int i = 1; i < parent.transform.childCount; i++)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }


        for (int i = 0; i < xRange; i++)
        {
            for (int j = 0; j < yRange; j++)
            {
                GameObject g = Instantiate(cell, parent.transform);
                g.SetActive(true);
                cells.Add(g);
            }
        }

        if (y < 12)
        {
            if(x<8)
                x++;
            y++;
        }
        else
        {
            x = 2;
            y = 2;
        }
       
    }


}
