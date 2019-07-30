using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class GameController : MonoBehaviour
{
 

    [SerializeField] List<ShapeController> activeElements = new List<ShapeController>();


    [SerializeField] private StringScriptable currentStyle;

    [SerializeField] GameObject shapeParent;


    int indexOfShapePrefub = 0;

    public int ActiveCount => activeElements.Count(entry => entry.isOn);

    public void CreateShapes(int elementsAmount)
    {

        var path = "Shapes";
        List< ShapeController> shapePrefab = null;

        Debug.Log(currentStyle.Value);

        if (!string.IsNullOrEmpty(currentStyle.Value))
        {
            shapePrefab = Resources.LoadAll<ShapeController>(Path.Combine(path, currentStyle.Value)).ToList();

            if (shapePrefab == null)
            {
                Debug.LogWarning($"Block prefab for style {currentStyle} is absent");
            }
        }

        if (shapePrefab == null)
        {
            shapePrefab = Resources.LoadAll<ShapeController>(Path.Combine(path, "dafault")).ToList();
        }

        if (shapePrefab == null)
        {
            Debug.LogError("default Block prefab is absent");
            return;
        }


        activeElements.ForEach(entry =>  Destroy( entry.gameObject));

        activeElements.Clear();


        while (activeElements.Count < elementsAmount)
        {
            if (indexOfShapePrefub >= shapePrefab.Count)
                indexOfShapePrefub = 0;
            var go = Instantiate(shapePrefab[indexOfShapePrefub], shapeParent.transform);
            indexOfShapePrefub++;
            activeElements.Add(go);        
        }

    }

    
}
