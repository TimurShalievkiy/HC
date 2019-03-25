using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class GameController : MonoBehaviour
{
    [SerializeField] List<ShapeController> activeElements = new List<ShapeController>();


    [SerializeField] private StringScriptableTest currentStyle;

    GameObject shapeParent;

    private void Update()
    {
        Debug.Log(ActiveCount);
    }

    public int ActiveCount => activeElements.Count(entry => entry.isOn);

    public void Init(int elementsAmount)
    {

        var path = "Shapes";

        List< ShapeController> shapePrefab = null;

        if (!string.IsNullOrEmpty(currentStyle.Value)) {
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
        activeElements.ForEach(entry => Destroy(entry.gameObject));

        int index = 0;


        while (activeElements.Count < elementsAmount)
        {
            var go = Instantiate(shapePrefab[index++], shapeParent.transform);
            activeElements.Add(go);
        }

    }
}
