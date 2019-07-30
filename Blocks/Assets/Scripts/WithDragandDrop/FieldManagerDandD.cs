using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldManagerDandD : MonoBehaviour
{

    //[SerializeField] Transform[] cellsPosArray;
    [SerializeField] Cell[] cellsStatePosArray;

    List<int> listIndexForShadow = new List<int>();
    List<int> listIndexWithShadow = new List<int>();






    //public void CheckFieldState()
    //{
    //    for (int i = 0; i < cellsStatePosArray.Length; i++)
    //    {
    //        cellsStatePosArray[i].InitializeCellPositionValue();
    //    }
    //}
    
    public bool CheckForInstance(List<int> posActivBlockInArr)
    {
        for (int i = 0; i < cellsStatePosArray.Length; i++)
        {
            if (Vector3.Distance(cellsStatePosArray[i].GetCerrentPosition(), TouchZoneWithDragAndDrop.firsBlock.position) < 30)
            {
                listIndexForShadow.Clear();
                if (posActivBlockInArr.Count == 1)
                {
                    if (cellsStatePosArray[i].isSet)
                        return false;

                    listIndexForShadow.Add(i);
                    return true;
                    //cellsStatePosArray[i].transform.GetComponent<Image>().color = Color.red; 
                }
                else
                {
                    int x1 = i / 10;
                    int y1 = i % 10;
                    int x2 = posActivBlockInArr[0] / 5;
                    int y2 = posActivBlockInArr[0] % 5;

                    int line = (i / 10 - posActivBlockInArr[0] / 5);
                    for (int j = 0; j < posActivBlockInArr.Count; j++)
                    {
                        if (j == 0)
                        {
                            if (cellsStatePosArray[i].isSet)
                            {
                               // Debug.Log("if (cellsStatePosArray[i].isSet)");
                                return false;
                            }
                            listIndexForShadow.Add(i);
                        }
                        else
                        {
                            int x3 = (x2 - posActivBlockInArr[j] / 5) * -1;
                            int y3 = (y2 - posActivBlockInArr[j] % 5) * -1;

                            int res = (x1 + x3) * 10 + y1 + y3;
                            
                            if (res < 0 || res > 99)
                            {
                                //Debug.Log(" if (res < 0 || res > 99)");
                                return false;
                            }

                            if (cellsStatePosArray[res].isSet)
                            {
                                //Debug.Log("if (cellsStatePosArray[res].isSet)");
                                return false;
                            }
                            if (line != (res / 10 - posActivBlockInArr[j] / 5))
                            {
                                //Debug.Log(" if (line != (res / 10 - posActivBlockInArr[j] / 5))");
                                return false;
                            }
                            listIndexForShadow.Add(res);
                            //cellsStatePosArray[res].transform.GetComponent<Image>().color = Color.red;
                        }
                    }
                   // Debug.Log(listIndexForShadow.Count);
                    return true;
                }
            }
        }
        return false;
    }

    public void CreateShadow()
    {
        ClearFieldFromShadow();
       // Debug.Log(listIndexWithShadow.Count + " " + listIndexWithShadow.Count);
        for (int i = 0; i < listIndexForShadow.Count; i++)
        {
            listIndexWithShadow.Add(listIndexForShadow[i]);
            cellsStatePosArray[listIndexForShadow[i]].GetComponent<Image>().color = Color.red;
        }
    }

    public void ClearFieldFromShadow()
    {
        for (int i = 0; i < listIndexWithShadow.Count; i++)
        {
            cellsStatePosArray[listIndexWithShadow[i]].GetComponent<Image>().color = Color.white;
        }
        listIndexWithShadow.Clear();
    }
}
